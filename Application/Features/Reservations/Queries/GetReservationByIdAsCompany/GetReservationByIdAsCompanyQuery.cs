using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetReservationByIdAsCompany
{
    public class GetReservationByIdAsCompanyQuery : IRequest<ReservationViewModel>
    {
        public int Id { get; set; }
    }
}
