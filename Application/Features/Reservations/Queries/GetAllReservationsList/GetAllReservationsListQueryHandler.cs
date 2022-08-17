using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetAllReservationsList
{
    public class GetAllReservationsListQueryHandler : IRequestHandler<GetAllReservationsListQuery, IEnumerable<ReservationViewModel>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public GetAllReservationsListQueryHandler(ICurrentUserService userService, IMapper mapper, IReservationRepository reservationRepository)
        {
            _userService = userService;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }
        public async Task<IEnumerable<ReservationViewModel>> Handle(GetAllReservationsListQuery request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;

            var reservationList = await _reservationRepository.GetAllReservationsForCustomerIdAsync(customerId);

            var mappedResult = _mapper.Map<IEnumerable<ReservationViewModel>>(reservationList);

            return mappedResult;
        }
    }
}
