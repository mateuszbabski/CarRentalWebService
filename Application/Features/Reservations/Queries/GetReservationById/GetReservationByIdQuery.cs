using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Queries.GetReservationById
{
    public class GetReservationByIdQuery : IRequest<ReservationViewModel>
    {
        public int Id { get; set; }
    }
}
