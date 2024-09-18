using AutoMapper;
using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Contracts.InvoiceItem;
using PosSystem.Application.Contracts.Product;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
    {
        public class InvoiceProfile : Profile
        {
            public InvoiceProfile()
            {
                CreateMap<InvoiceCreationContract, Invoice>();

               
                CreateMap<InvoiceCreationContract, Invoice>()
                    .ForMember(dest => dest.Items, opt => opt.Ignore()); 

                CreateMap<Invoice, InvoiceOutContract>()
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.User.FullName));

                CreateMap<InvoiceItem, InvoiceItemOutContract>()
                    .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Product.Name))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalAmount));

                CreateMap<Invoice, InvoiceOutContract>()
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                    .ForMember(dest => dest.BillDate, opt => opt.MapFrom(src => src.BillDate));
            }
        }
    }

