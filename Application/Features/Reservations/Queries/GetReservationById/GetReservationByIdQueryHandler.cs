using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationViewModel>
    {
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public GetReservationByIdQueryHandler(ICurrentUserService userService, IMapper mapper, IReservationRepository reservationRepository)
        {
            _userService = userService;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }
        public async Task<ReservationViewModel> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;

            var reservation = await _reservationRepository.GetReservationByIdForCustomerAsync(customerId, request.Id);
            if (reservation == null)
                throw new NotFoundException();

            return _mapper.Map<ReservationViewModel>(reservation);
        }
    }
}
