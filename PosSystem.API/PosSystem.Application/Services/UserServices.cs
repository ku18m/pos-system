using AutoMapper;
using PosSystem.Application.Contracts.User;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services
{
    public class UserServices(IUnitOfWork unitOfWork, IMapper mapper) : IUserServices
    {
        public async Task<UserOutContract?> CreateUserAsync(UserCreationContract userFromRequest)
        {
            userFromRequest.Password = BCrypt.Net.BCrypt.HashPassword(userFromRequest.Password);

            var userToInsert = mapper.Map<User>(userFromRequest);

            await unitOfWork.UserRepository.Insert(userToInsert);

            try
            {
                await unitOfWork.Save();
            }
            catch (Exception)
            {
                return null;
            }

            var userToReturn = mapper.Map<UserOutContract>(userToInsert);

            return userToReturn;
        }

        public async Task<int> DeleteUserByIdAsync(string id)
        {
            int result;

            await unitOfWork.UserRepository.Delete(id);

            try
            {
                result = await unitOfWork.Save();
            }
            catch (Exception)
            {
                return 500;
            }

            if (result == 0)
            {
                return 404;
            }

            return 200;
        }

        public async Task<IEnumerable<UserOutContract>> GetAllUsersAsync()
        {
            var users = await unitOfWork.UserRepository.GetAll();

            var usersToReturn = mapper.Map<IEnumerable<UserOutContract>>(users);

            return usersToReturn;
        }

        public async Task<UserOutContract?> GetUserByIdAsync(string Id)
        {
            var user = await unitOfWork.UserRepository.GetById(Id);

            if (user == null)
            {
                return null;
            }

            var userToReturn = mapper.Map<UserOutContract>(user);

            return userToReturn;
        }

        public async Task<UserOutContract?> GetUserByUsernameAsync(string username)
        {
            var user = await unitOfWork.UserRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                return null;
            }

            var userToReturn = mapper.Map<UserOutContract>(user);

            return userToReturn;
        }

        public async Task<UserOutContract?> UpdateUserAsync(string userId, UserOperationsContract userToUpdate)
        {
            var user = await unitOfWork.UserRepository.GetById(userId);

            if (user == null)
            {
                return null;
            }

            userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(userToUpdate.Password);

            var userToUpdateEntity = mapper.Map(userToUpdate, user);

            await unitOfWork.UserRepository.Update(userToUpdateEntity);

            try
            {
                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var userToReturn = mapper.Map<UserOutContract>(userToUpdateEntity);

            return userToReturn;
        }

        public async Task<bool> UpdateUserPassword(string userId, string newPassword)
        {
            var user = await unitOfWork.UserRepository.GetById(userId);

            if (user == null)
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await unitOfWork.UserRepository.Update(user);

            try
            {
                await unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }
    }
}
