﻿using PosSystem.Core.Entities;

namespace PosSystem.Application.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUserNameAsync(string username);
    }
}
