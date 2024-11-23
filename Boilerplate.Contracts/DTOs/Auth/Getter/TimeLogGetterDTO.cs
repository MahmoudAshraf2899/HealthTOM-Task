
namespace Boilerplate.Contracts.DTOs.Auth.Getter
{
    public class TimeLogGetterDTO
    {
        public long Id { get; set; } = 0;
        public DateTime CheckTime { get; set; }
        public string? CreatedBy { get; set; }

    }
}
