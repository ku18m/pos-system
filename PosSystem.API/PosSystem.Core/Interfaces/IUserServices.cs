namespace PosSystem.Core.Interfaces
{
    public interface IUserServices<TUserIn, TUserOut> where TUserIn : class where TUserOut : class
    {
        Task<TUserOut> CreateUserAsync(TUserIn user);
        Task<TUserOut> GetUserAsync();
        Task<TUserOut> UpdateUserAsync(string userId, TUserIn userToUpdate);
        Task<TUserOut> DeleteUserByIdAsync(string id);
        Task<TUserOut> GetUserByIdAsync(string Id);
        Task<IEnumerable<TUserOut>> GetAllUsersAsync();
        Task<TUserOut> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserPassword(string userId, string newPassword);
        Task<bool> UpdateCurrentUserPassword(string oldPassword, string newPassword);
    }
}
