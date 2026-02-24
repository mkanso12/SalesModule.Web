using AutoMapper;
using SalesModule.API.Models;
using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesModule.API.MappingProfiles
{
    public class SalesModuleProfile : Profile
    {
        public SalesModuleProfile()
        {
            // Payment -> PaymentDto mapping
            CreateMap<Payment, PaymentDto>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : null))
                .ForMember(
                    dest => dest.InvoiceStatus,
                    opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.Status : null));
        }
    }
}