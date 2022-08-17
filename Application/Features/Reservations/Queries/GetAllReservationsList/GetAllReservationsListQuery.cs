using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetAllReservationsList
{
    public class GetAllReservationsListQuery : IRequest<IEnumerable<ReservationViewModel>>
    {
    }
}
