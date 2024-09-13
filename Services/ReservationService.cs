using Microsoft.EntityFrameworkCore;
using WebApiResto.Data;
using WebApiResto.DTOs;
using WebApiResto.Models;

namespace WebApiResto.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationDTO>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Select(reservation => new ReservationDTO
                {
                    Id = reservation.Id,
                    ReservationDate = reservation.ReservationDate,
                    NumberOfGuests = reservation.NumberOfGuests,
                    UserId = reservation.UserId,
                    RestaurantId = reservation.RestaurantId
                })
                .ToListAsync();
        }

        public async Task<ReservationDTO?> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return null;

            return new ReservationDTO
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                NumberOfGuests = reservation.NumberOfGuests,
                UserId = reservation.UserId,
                RestaurantId = reservation.RestaurantId
            };
        }

        public async Task<ReservationDTO> CreateReservationAsync(ReservationDTO reservationDTO)
        {
            var reservation = new Reservation
            {
                ReservationDate = reservationDTO.ReservationDate,
                NumberOfGuests = reservationDTO.NumberOfGuests,
                UserId = reservationDTO.UserId,
                RestaurantId = reservationDTO.RestaurantId
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            reservationDTO.Id = reservation.Id;
            return reservationDTO;
        }

        public async Task<ReservationDTO?> UpdateReservationAsync(ReservationDTO reservationDTO)
        {
            var reservation = await _context.Reservations.FindAsync(reservationDTO.Id);
            if (reservation == null) return null;

            reservation.ReservationDate = reservationDTO.ReservationDate;
            reservation.NumberOfGuests = reservationDTO.NumberOfGuests;

            await _context.SaveChangesAsync();

            return reservationDTO;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
