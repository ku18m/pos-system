using AutoMapper;
using PosSystem.Contracts.User;
using PosSystem.Core.Entities;

namespace PosSystem.Services.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserOperationsContract, User>();

            CreateMap<User, UserOutContract>();
        }
    }
}
