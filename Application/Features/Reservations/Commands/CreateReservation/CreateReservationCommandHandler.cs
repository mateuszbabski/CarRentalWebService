using Application.Exceptions;
using Application.Features.Reservations;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservationViewModel>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalCompanyRepository _companyRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;

        public CreateReservationCommandHandler(
            IVehicleRepository vehicleRepository,
            IRentalCompanyRepository companyRepository,
            ICustomerRepository customerRepository,
            IReservationRepository reservationRepository,
            IMapper mapper,
            ICurrentUserService userService)
        {
            _vehicleRepository = vehicleRepository;
            _companyRepository = companyRepository;
            _customerRepository = customerRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ReservationViewModel> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var customer = await _customerRepository.GetCustomerByIdAsync(userId);
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            var company = await _companyRepository.GetRentalCompanyByIdAsync(vehicle.RentalCompanyId);

            if (customer == null || vehicle == null || company == null)
                throw new NotFoundException();

            if (vehicle.IsAvailable == false)
                throw new Exception("Car is not available");

            var newReservation = new Reservation()
            {
                ReservationStart = request.ReservationStart,
                ReservationEnd = request.ReservationEnd,
                Status = "Pending",
                DailyCost = vehicle.DailyCost,
                Vehicle = vehicle,
                RentalCompany = company,
                Customer = customer,
            };

            var reservationRepo = await _reservationRepository.AddAsync(newReservation);

            if (reservationRepo == null)
                throw new Exception("Internal problem with DB save");

            var reservationVM = new ReservationViewModel()
            {
                ReservationStart = request.ReservationStart,
                ReservationEnd = request.ReservationEnd,
                Status = "Pending",
                DailyCost = vehicle.DailyCost,
                CustomerFirstName = customer.FirstName,
                CustomerLastName = customer.LastName,
                CompanyName = company.CompanyName,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Type = vehicle.Type,
                NumberOfSeats = vehicle.NumberOfSeats,
                FuelType = vehicle.FuelType,
                ProductionYear = vehicle.ProductionYear,
                Color = vehicle.Color
            };

            return reservationVM;
        }
    }
}


