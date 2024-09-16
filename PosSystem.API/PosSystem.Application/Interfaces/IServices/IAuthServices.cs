namespace PosSystem.Application.Interfaces.IServices
{
    public interface IAuthServices
    {
        Task<string> LoginUserAsync(string email, string password);
    }
}
