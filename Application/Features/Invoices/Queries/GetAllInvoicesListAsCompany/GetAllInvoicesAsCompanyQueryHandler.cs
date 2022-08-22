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

namespace Application.Features.Invoices.Queries.GetAllInvoicesListAsCompany
{
    public class GetAllInvoicesAsCompanyQueryHandler : IRequestHandler<GetAllInvoicesAsCompanyQuery, IEnumerable<InvoiceViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;
        private readonly IInvoiceRepository _invoiceRepository;

        public GetAllInvoicesAsCompanyQueryHandler(IMapper mapper, ICurrentUserService userService, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<IEnumerable<InvoiceViewModel>> Handle(GetAllInvoicesAsCompanyQuery request, CancellationToken cancellationToken)
        {
            var companyId = _userService.UserId;

            var invoice = await _invoiceRepository.GetAllInvoicesForCompanyAsync(companyId);
            if (invoice == null)
                throw new NotFoundException();

            return _mapper.Map<IEnumerable<InvoiceViewModel>>(invoice);
        }
    }
}
