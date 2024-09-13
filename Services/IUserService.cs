using WebApiResto.DTOs;

namespace WebApiResto.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);  // Cambiamos a UserDTO? para indicar posible nulidad
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDTO?> UpdateUserAsync(UserDTO updatedUserDTO);  // Cambiamos a UserDTO?
        Task<bool> DeleteUserAsync(int id);
    }
}
