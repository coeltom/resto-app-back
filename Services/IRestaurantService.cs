using WebApiResto.DTOs;

namespace WebApiResto.Services
{
    public interface IRestaurantService
    {
        Task<List<RestaurantDTO>> GetAllRestaurantsAsync();
        Task<RestaurantDTO?> GetRestaurantByIdAsync(int id);  // Cambiamos a RestaurantDTO? para reflejar la nulabilidad
        Task<RestaurantDTO> CreateRestaurantAsync(RestaurantDTO restaurantDTO);
        Task<RestaurantDTO?> UpdateRestaurantAsync(RestaurantDTO restaurantDTO);  // Cambiamos a RestaurantDTO?
        Task<bool> DeleteRestaurantAsync(int id);
    }
}
