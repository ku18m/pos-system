using AutoMapper;
using PosSystem.Application.Contracts.Unit;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            CreateMap<UnitCreationContract, Unit>();

            CreateMap<UnitOperationsContract, Unit>()
                .ForMember(dest => dest.UnitId, opt => opt.Ignore());

            CreateMap<Unit, UnitOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UnitId));

            CreateMap<Unit, UnitShortOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UnitId));
        }
    }
}
