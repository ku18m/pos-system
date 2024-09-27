using AutoMapper;
using PosSystem.Application.Contracts.InvoiceItem;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class InvoiceItemProfile : Profile
    {
        public InvoiceItemProfile()
        {
            CreateMap<InvoiceItemCreationContract, InvoiceItem>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemId))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SellingPrice));

            CreateMap<InvoiceItemOperationsContract, InvoiceItem>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvoiceItemId))
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemId))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SellingPrice));

            CreateMap<InvoiceItem, InvoiceItemOutContract>()
                    .ForMember(dest => dest.InvoiceItemId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Product.Name))
                    .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ProductId))
                    .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.Name))
                    .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.UnitId))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalAmount))
                    .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.Price));

            CreateMap<InvoiceItem, InvoiceItemShortOutContract>()
                    .ForMember(dest => dest.InvoiceItemId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
