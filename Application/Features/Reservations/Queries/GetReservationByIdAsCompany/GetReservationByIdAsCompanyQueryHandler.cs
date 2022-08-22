using Application.Exceptions;
using Application.Features.Reservations.Queries.GetAllReservationListAsCompany;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetReservationByIdAsCompany
{
    public class GetReservationByIdAsCompanyQueryHandler : IRequestHandler<GetReservationByIdAsCompanyQuery, ReservationViewModel>
    {
        private readonly ICurrentUserService _userService;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public GetReservationByIdAsCompanyQueryHandler(ICurrentUserService userService, IMapper mapper, IReservationRepository reservationRepository)
        {
            _userService = userService;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationViewModel> Handle(GetReservationByIdAsCompanyQuery request, CancellationToken cancellationToken)
        {
            var companyId = _userService.UserId;

            var reservation = await _reservationRepository.GetReservationByIdForCompanyAsync(companyId, request.Id);
            if (reservation == null)
                throw new NotFoundException();

            return _mapper.Map<ReservationViewModel>(reservation);
        }
    }
}
