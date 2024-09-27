using AutoMapper;
using PosSystem.Application.Contracts.Invoice;
using PosSystem.Application.Contracts.InvoiceItem;
using PosSystem.Core.Entities;

namespace PosSystem.Application.Services.AutoMapper
{
    public class InvoiceProfile : Profile
        {
            public InvoiceProfile()
            {
                CreateMap<InvoiceCreationContract, Invoice>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.InvoiceItems))
                    .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(src => src.PaidUp))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.EmployeeId));

                CreateMap<InvoiceOperationsContract, Invoice>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.InvoiceItems))
                    .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(src => src.PaidUp))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.EmployeeId));


                CreateMap<Invoice, InvoiceOutContract>()
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                    .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.User.FullName))
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.BillNumber))
                    .ForMember(dest => dest.Net, opt => opt.MapFrom(src => src.FinalAmount))
                    .ForMember(dest => dest.PaidUp, opt => opt.MapFrom(src => src.PaidAmount))
                    .ForMember(dest => dest.Remaining, opt => opt.MapFrom(src => src.DueAmount))
                    .ForMember(dest => dest.InvoiceItems, opt => opt.MapFrom(src => src.Items));

                CreateMap<Invoice, InvoiceShortOutContract>()
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.User.FullName))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.BillNumber))
                    .ForMember(dest => dest.Net, opt => opt.MapFrom(src => src.FinalAmount))
                    .ForMember(dest => dest.PaidUp, opt => opt.MapFrom(src => src.PaidAmount))
                    .ForMember(dest => dest.DiscountValue, opt => opt.MapFrom(src => src.TotalDiscount));
            }
        }
    }

