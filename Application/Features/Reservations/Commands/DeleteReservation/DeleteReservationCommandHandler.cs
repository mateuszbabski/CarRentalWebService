using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationCommandHandler(ICurrentUserService userService, IReservationRepository reservationRepository)
        {
            _userService = userService;
            _reservationRepository = reservationRepository;
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var reservation = await _reservationRepository.GetByIdAsync(request.Id);
            if (reservation == null || reservation.CustomerId != userId)
                throw new NotFoundException("Reservation not found or you dont have an access");

            await _reservationRepository.DeleteAsync(reservation);

            return Unit.Value;
        }
    }
}
