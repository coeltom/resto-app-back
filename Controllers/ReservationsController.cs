using Microsoft.AspNetCore.Mvc;
using WebApiResto.Services;
using WebApiResto.DTOs;
using System.Threading.Tasks;

namespace WebApiResto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDTO reservationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdReservation = await _reservationService.CreateReservationAsync(reservationDTO);
            return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationDTO reservationDTO)
        {
            if (id != reservationDTO.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedReservation = await _reservationService.UpdateReservationAsync(reservationDTO);
            if (updatedReservation == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

