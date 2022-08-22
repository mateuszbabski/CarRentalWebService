using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetAllReservationListAsCompany
{
    public class GetAllReservationListAsCompanyQueryHandler : IRequestHandler<GetAllReservationListAsCompanyQuery, IEnumerable<ReservationViewModel>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public GetAllReservationListAsCompanyQueryHandler(ICurrentUserService userService, IMapper mapper, IReservationRepository reservationRepository)
        {
            _userService = userService;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationViewModel>> Handle(GetAllReservationListAsCompanyQuery request, CancellationToken cancellationToken)
        {
            var companyId = _userService.UserId;

            var reservationList = await _reservationRepository.GetAllReservationsForCompanyIdAsync(companyId);
            if (reservationList == null)
                throw new NotFoundException();

            var mappedResult = _mapper.Map<IEnumerable<ReservationViewModel>>(reservationList);

            return mappedResult;
        }
    }
}
