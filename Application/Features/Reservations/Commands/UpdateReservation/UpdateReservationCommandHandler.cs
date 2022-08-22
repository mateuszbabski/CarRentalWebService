using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationCommandHandler(ICurrentUserService userService, IReservationRepository reservationRepository)
        {
            _userService = userService;
            _reservationRepository = reservationRepository;
        }

        public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var reservation = await _reservationRepository.GetReservationByIdForCustomerAsync(customerId, request.Id);
            if (reservation == null)
                throw new NotFoundException();

            reservation.ReservationStart = request.ReservationStart;
            reservation.ReservationEnd = request.ReservationEnd;

            await _reservationRepository.UpdateAsync(reservation);

            return Unit.Value;
        }
    }
}
