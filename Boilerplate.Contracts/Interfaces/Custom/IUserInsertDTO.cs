namespace Boilerplate.Contracts.Interfaces.Custom
{
    public interface IUserInsertDTO
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
