using Microsoft.AspNetCore.Mvc;
using WebApiResto.Services;
using WebApiResto.DTOs;
using System.Threading.Tasks;

namespace WebApiResto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  // Asegúrate de que solo esté declarado una vez
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        // El constructor solo debe existir una vez y recibir las dependencias necesarias
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUserAsync(userDTO);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO updatedUserDTO)
        {
            if (id != updatedUserDTO.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedUser = await _userService.UpdateUserAsync(updatedUserDTO);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
