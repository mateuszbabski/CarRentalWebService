using Application.Exceptions;
using Application.Features.Invoice;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;
        private readonly IReservationRepository _reservationRepository;

        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper, ICurrentUserService userService, IReservationRepository reservationRepository)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _userService = userService;
            _reservationRepository = reservationRepository;
        }
        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var reservation = _reservationRepository.GetReservationByIdForCompanyAsync(userId, request.ReservationId);
            if (reservation == null)
                throw new NotFoundException("Reservation does not exist");

            var rentDuration = Convert.ToDecimal(reservation.Result.ReservationEnd.DayOfYear - reservation.Result.ReservationStart.DayOfYear);

            var newInvoice = new Domain.Entities.Invoice()
            {
                Customer = reservation.Result.Customer,
                Vehicle = reservation.Result.Vehicle,
                RentalCompany = reservation.Result.RentalCompany,
                Reservation = reservation.Result,
                FullPriceForRenting = rentDuration * reservation.Result.Vehicle.DailyCost,
                IsPaid = false
            };

            await _invoiceRepository.AddAsync(newInvoice);

            return newInvoice.Id;
        }
    }
}
