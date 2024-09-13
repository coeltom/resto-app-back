namespace WebApiResto.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
    }
}
