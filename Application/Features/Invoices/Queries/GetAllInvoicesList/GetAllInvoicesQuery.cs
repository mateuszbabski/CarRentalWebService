using Application.Features.Invoice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Queries.GetAllInvoicesList
{
    public class GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceViewModel>>
    {
    }
}
