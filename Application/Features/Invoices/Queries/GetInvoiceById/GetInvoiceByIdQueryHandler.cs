using Application.Exceptions;
using Application.Features.Invoice;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceByIdQueryHandler(IMapper mapper, ICurrentUserService userService, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceViewModel> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;

            var invoice = await _invoiceRepository.GetInvoiceByIdForCustomerAsync(customerId, request.Id);
            if (invoice == null)
                throw new NotFoundException();

            return _mapper.Map<InvoiceViewModel>(invoice);
        }
    }
}
