using Application.Authentication;
using Application.Features.Invoice;
using Application.Features.RentalCompanies;
using Application.Features.Reservations;
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

            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(c => c.Brand, c => c.MapFrom(s => s.Vehicle.Brand))
                .ForMember(c => c.Model, c => c.MapFrom(s => s.Vehicle.Model))
                .ForMember(c => c.Type, c => c.MapFrom(s => s.Vehicle.Type))
                .ForMember(c => c.Color, c => c.MapFrom(s => s.Vehicle.Color))
                .ForMember(c => c.ProductionYear, c => c.MapFrom(s => s.Vehicle.ProductionYear))
                .ForMember(c => c.DailyCost, c => c.MapFrom(s => s.Vehicle.DailyCost))
                .ForMember(c => c.FuelType, c => c.MapFrom(s => s.Vehicle.FuelType))
                .ForMember(c => c.NumberOfSeats, c => c.MapFrom(s => s.Vehicle.NumberOfSeats))
                .ForMember(c => c.CustomerFirstName, c => c.MapFrom(s => s.Customer.FirstName))
                .ForMember(c => c.CustomerLastName, c => c.MapFrom(s => s.Customer.LastName))
                .ForMember(c => c.CompanyName, c => c.MapFrom(s => s.RentalCompany.CompanyName))
                .ForMember(c => c.ReservationStart, c => c.MapFrom(s => s.ReservationStart))
                .ForMember(c => c.ReservationEnd, c => c.MapFrom(s => s.ReservationEnd))
                .ForMember(c => c.Id, c => c.MapFrom(s => s.Id));

            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(c => c.Brand, c => c.MapFrom(s => s.Vehicle.Brand))
                .ForMember(c => c.Model, c => c.MapFrom(s => s.Vehicle.Model))
                .ForMember(c => c.Type, c => c.MapFrom(s => s.Vehicle.Type))
                .ForMember(c => c.ProductionYear, c => c.MapFrom(s => s.Vehicle.ProductionYear))
                .ForMember(c => c.FuelType, c => c.MapFrom(s => s.Vehicle.FuelType))
                .ForMember(c => c.CustomerFirstName, c => c.MapFrom(s => s.Customer.FirstName))
                .ForMember(c => c.CustomerLastName, c => c.MapFrom(s => s.Customer.LastName))
                .ForMember(c => c.CompanyName, c => c.MapFrom(s => s.RentalCompany.CompanyName))
                .ForMember(c => c.CompanyIdentificationNumber, c => c.MapFrom(s => s.RentalCompany.CompanyIdentificationNumber))
                .ForMember(c => c.ReservationStart, c => c.MapFrom(s => s.Reservation.ReservationStart))
                .ForMember(c => c.ReservationEnd, c => c.MapFrom(s => s.Reservation.ReservationEnd))
                .ForMember(c => c.ReservationId, c => c.MapFrom(s => s.Reservation.Id))
                .ForMember(c => c.Id, c => c.MapFrom(s => s.Id))
                .ForMember(c => c.FullPriceForRenting, c => c.MapFrom(s => s.FullPriceForRenting))
                .ForMember(c => c.IsPaid, c => c.MapFrom(s => s.IsPaid));



        }
    }
}



