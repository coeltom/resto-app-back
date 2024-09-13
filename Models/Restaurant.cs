namespace WebApiResto.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
