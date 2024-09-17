using AutoMapper;
using PosSystem.Application.Contracts.Client;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientCreationContract, Client>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<ClientOperationsContract, Client>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.ClientId, opt => opt.Ignore());

            CreateMap<Client, ClientOutContract>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId));

            CreateMap<Client, ClientShortOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId));
        }
    }
}
