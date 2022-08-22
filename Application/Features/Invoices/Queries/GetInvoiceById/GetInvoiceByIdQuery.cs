using Application.Features.Invoice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceViewModel>
    {
        public int Id { get; set; }
    }
}
