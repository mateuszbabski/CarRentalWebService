using Application.Authentication;
using Application.Features.RentalCompany;
using Application.Features.Vehicles;
using Application.Features.Vehicles.Commands.CreateVehicle;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterCustomerRequest, Customer>();
            //    .ForMember(m => m.Country, c => c.MapFrom(s => s.Address.Country))
            //    .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
            //    .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
            //    .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<RegisterRentalCompanyRequest, RentalCompany>();
            //    .ForMember(m => m.Country, c => c.MapFrom(s => s.Address.Country))
            //    .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
            //    .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
            //    .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<CreateVehicleCommand, Vehicle>().ReverseMap();

            CreateMap<VehicleViewModel, Vehicle>().ReverseMap();

            CreateMap<RentalCompanyViewModel, RentalCompany>().ReverseMap();
                
                
        }
    }
}
