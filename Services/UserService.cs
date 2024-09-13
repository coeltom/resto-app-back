using Microsoft.EntityFrameworkCore;
using WebApiResto.Data;
using WebApiResto.DTOs;
using WebApiResto.Models;

namespace WebApiResto.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username ?? "N/A",  // Controlamos posibles nulos
                    FullName = user.FullName ?? "N/A",
                    Email = user.Email ?? "N/A"
                })
                .ToListAsync();
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username ?? "N/A",
                FullName = user.FullName ?? "N/A",
                Email = user.Email ?? "N/A"
            };
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            var user = new User
            {
                Username = userDTO.Username ?? "N/A",
                FullName = userDTO.FullName ?? "N/A",
                Email = userDTO.Email ?? "N/A",
                PasswordHash = "hashed_password"  // Lógica de hashing de contraseña
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDTO.Id = user.Id;
            return userDTO;
        }

        public async Task<UserDTO?> UpdateUserAsync(UserDTO updatedUserDTO)
        {
            var user = await _context.Users.FindAsync(updatedUserDTO.Id);
            if (user == null) return null;

            user.Username = updatedUserDTO.Username ?? "N/A";
            user.FullName = updatedUserDTO.FullName ?? "N/A";
            user.Email = updatedUserDTO.Email ?? "N/A";

            await _context.SaveChangesAsync();

            return updatedUserDTO;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
