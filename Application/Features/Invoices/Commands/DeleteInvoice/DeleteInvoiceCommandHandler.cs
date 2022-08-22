using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IInvoiceRepository _invoiceRepository;

        public DeleteInvoiceCommandHandler(ICurrentUserService userService, IInvoiceRepository invoiceRepository)
        {
            _userService = userService;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var companyId = _userService.UserId;

            var invoice = await _invoiceRepository.GetInvoiceByIdForCompanyAsync(companyId, request.Id);
            if (invoice == null)
                throw new NotFoundException("Reservation not found or you dont have an access");

            await _invoiceRepository.DeleteAsync(invoice);

            return Unit.Value;
        }
    }
}
