using AutoMapper;
using PosSystem.Application.Contracts.Company;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyCreationContract, Company>();

            CreateMap<CompanyOperationsContract, Company>()
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore());

            CreateMap<Company, CompanyOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CompanyId));

            CreateMap<Company, CompanyShortOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CompanyId));
        }
    }
}
