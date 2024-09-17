using PosSystem.Application.Contracts.User;

namespace PosSystem.Application.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<UserOutContract?> CreateUserAsync(UserCreationContract user);
        Task<UserOutContract?> UpdateUserAsync(string userId, UserOperationsContract userToUpdate);
        Task<int> DeleteUserByIdAsync(string id);
        Task<UserOutContract?> GetUserByIdAsync(string Id);
        Task<IEnumerable<UserOutContract>> GetAllUsersAsync();
        Task<UserOutContract?> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserPassword(string userId, string newPassword);
    }
}
