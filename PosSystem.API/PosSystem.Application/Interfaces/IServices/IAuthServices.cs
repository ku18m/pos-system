namespace PosSystem.Application.Interfaces.IServices
{
    public interface IAuthServices
    {
        Task<string?> LoginUserAsync(string username, string password);
    }
}
