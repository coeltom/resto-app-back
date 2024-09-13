namespace WebApiResto.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
    }
}
