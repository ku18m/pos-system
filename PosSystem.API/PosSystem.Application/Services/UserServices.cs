using AutoMapper;
using Microsoft.AspNetCore.Http;
using PosSystem.Application.Contracts.User;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services
{
    public class UserServices<TUserIn, TUserOut>(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext) : IUserServices<UserOperationsContract, UserOutContract>
    {
        public async Task<UserOutContract> CreateUserAsync(UserOperationsContract userFromRequest)
        {
            userFromRequest.Password = BCrypt.Net.BCrypt.HashPassword(userFromRequest.Password);

            var userToInsert = mapper.Map<User>(userFromRequest);

            await unitOfWork.UserRepository.Insert(userToInsert);

            int result = 0;
            try
            {
                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return null;
            }

            var userToReturn = mapper.Map<UserOutContract>(userToInsert);

            return userToReturn;
        }

        public Task<UserOutContract> GetUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserOutContract> UpdateUserAsync(string userId, UserOperationsContract userToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<UserOutContract> DeleteUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserOutContract> GetUserByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserOutContract>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserOutContract> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserPassword(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCurrentUserPassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
