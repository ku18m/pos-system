using AutoMapper;
using PosSystem.Application.Contracts.Product;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreationContract, Product>();

            CreateMap<ProductOperationsContract, Product>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());

            CreateMap<Product, ProductOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.Name));

            CreateMap<Product, ProductShortOutContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
