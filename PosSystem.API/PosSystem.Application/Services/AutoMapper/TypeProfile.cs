using AutoMapper;
using PosSystem.Application.Contracts.Type;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<TypeCreationContract, Category>();

            CreateMap<TypeOperationsContract, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());

            CreateMap<Category, TypeOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));

            CreateMap<Category, TypeShortOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}
