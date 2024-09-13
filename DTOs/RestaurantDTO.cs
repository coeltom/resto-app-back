namespace WebApiResto.DTOs
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public int Capacity { get; set; }
    }
}
