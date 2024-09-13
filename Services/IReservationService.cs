using WebApiResto.DTOs;

namespace WebApiResto.Services
{
    public interface IReservationService
    {
        Task<List<ReservationDTO>> GetAllReservationsAsync();
        Task<ReservationDTO?> GetReservationByIdAsync(int id);  // Cambiamos a ReservationDTO?
        Task<ReservationDTO> CreateReservationAsync(ReservationDTO reservationDTO);
        Task<ReservationDTO?> UpdateReservationAsync(ReservationDTO reservationDTO);  // Cambiamos a ReservationDTO?
        Task<bool> DeleteReservationAsync(int id);
    }
}
