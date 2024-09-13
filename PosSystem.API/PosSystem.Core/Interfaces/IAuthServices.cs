using PosSystem.Core.Entities;

namespace PosSystem.Core.Interfaces
{
    public interface IAuthServices
    {
        Task<string> LoginUserAsync(string email, string password);
    }
}
