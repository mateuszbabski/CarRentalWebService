﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommand : IRequest
    {
        public int Id { get; set; }
    }
}
