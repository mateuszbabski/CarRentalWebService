using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetAllReservationListAsCompany
{
    public class GetAllReservationListAsCompanyQuery : IRequest<IEnumerable<ReservationViewModel>>
    {
    }
}
