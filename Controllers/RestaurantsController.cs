using Microsoft.AspNetCore.Mvc;
using WebApiResto.Services;
using WebApiResto.DTOs;
using System.Threading.Tasks;

namespace WebApiResto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null) return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDTO restaurantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRestaurant = await _restaurantService.CreateRestaurantAsync(restaurantDTO);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = createdRestaurant.Id }, createdRestaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantDTO restaurantDTO)
        {
            if (id != restaurantDTO.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(restaurantDTO);
            if (updatedRestaurant == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var result = await _restaurantService.DeleteRestaurantAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
