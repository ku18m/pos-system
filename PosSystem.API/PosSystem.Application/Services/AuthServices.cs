using PosSystem.Application.Interfaces.IServices;
using PosSystem.Application.Services.Helpers;

namespace PosSystem.Application.Services
{
    public class AuthServices(IUnitOfWork unitOfWork, TokenGeneratorHelper tokenGenerator) : IAuthServices
    {
        public async Task<string?> LoginUserAsync(string username, string password)
        {
            var user = await unitOfWork.UserRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var token = tokenGenerator.GenerateToken(user);

            return token;
        }
    }
}
