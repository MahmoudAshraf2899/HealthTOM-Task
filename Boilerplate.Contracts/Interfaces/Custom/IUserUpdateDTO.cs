namespace Boilerplate.Contracts.Interfaces.Custom
{
    public interface IUserUpdateDTO
    {
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
