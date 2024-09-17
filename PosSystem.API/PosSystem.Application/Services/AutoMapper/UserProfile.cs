using AutoMapper;
using PosSystem.Application.Contracts.User;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreationContract, User>();

            CreateMap<UserOperationsContract, User>();

            CreateMap<User, UserOutContract>();
        }
    }
}
