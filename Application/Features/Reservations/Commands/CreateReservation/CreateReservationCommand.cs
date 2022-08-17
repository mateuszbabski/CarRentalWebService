using Application.Features.Reservations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<ReservationViewModel>
    {
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public int VehicleId { get; set; }

    }
}

