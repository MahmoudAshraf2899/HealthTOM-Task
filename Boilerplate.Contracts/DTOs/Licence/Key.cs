namespace Boilerplate.Contracts.DTOs
{
    public class Key
    {
        public string? Status { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int Users { get; set; }
        public int TimeInDays { get; set; }
    }
}
