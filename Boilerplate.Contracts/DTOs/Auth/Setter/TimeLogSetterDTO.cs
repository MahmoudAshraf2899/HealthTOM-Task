
namespace Boilerplate.Contracts.DTOs.Auth.Setter
{
    public class TimeLogSetterDTO
    {
        public long Id { get; set; } = 0;
        public DateTime CheckTime { get; set; }
        public string? CreatedBy { get; set; }

    }
}
