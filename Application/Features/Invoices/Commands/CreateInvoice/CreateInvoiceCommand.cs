﻿using Application.Features.Invoice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public int ReservationId { get; set; }
    }
}
