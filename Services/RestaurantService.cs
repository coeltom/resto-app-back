using Microsoft.EntityFrameworkCore;
using WebApiResto.Data;
using WebApiResto.DTOs;
using WebApiResto.Models;

namespace WebApiResto.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext _context;

        public RestaurantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantDTO>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants
                .Select(restaurant => new RestaurantDTO
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name ?? "N/A",  // Controlamos posibles nulos
                    Location = restaurant.Location ?? "N/A",
                    Capacity = restaurant.Capacity
                })
                .ToListAsync();
        }

        public async Task<RestaurantDTO?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return null;

            return new RestaurantDTO
            {
                Id = restaurant.Id,
                Name = restaurant.Name ?? "N/A",
                Location = restaurant.Location ?? "N/A",
                Capacity = restaurant.Capacity
            };
        }

        public async Task<RestaurantDTO> CreateRestaurantAsync(RestaurantDTO restaurantDTO)
        {
            var restaurant = new Restaurant
            {
                Name = restaurantDTO.Name ?? "N/A",
                Location = restaurantDTO.Location ?? "N/A",
                Capacity = restaurantDTO.Capacity
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            restaurantDTO.Id = restaurant.Id;
            return restaurantDTO;
        }

        public async Task<RestaurantDTO?> UpdateRestaurantAsync(RestaurantDTO restaurantDTO)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantDTO.Id);
            if (restaurant == null) return null;

            restaurant.Name = restaurantDTO.Name ?? "N/A";
            restaurant.Location = restaurantDTO.Location ?? "N/A";
            restaurant.Capacity = restaurantDTO.Capacity;

            await _context.SaveChangesAsync();

            return restaurantDTO;
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
