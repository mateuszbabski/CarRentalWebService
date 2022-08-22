using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Reservations.Queries.GetReservationById;
using Application.Features.Reservations;
using Application.Features.Vehicles.Commands.CreateVehicle;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Invoices.Queries.GetInvoiceById;
using Application.Features.Invoice;
using Application.Features.Invoices.Queries.GetAllInvoicesList;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Company")]
        [HttpPost("CreateInvoice")]
        public async Task<ActionResult<int>> AddVehicle([FromBody] CreateInvoiceCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{id}", Name = "GetInvoicenById")]
        public async Task<ActionResult<InvoiceViewModel>> GetInvoiceById(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery()
            {
                Id = id
            });

            return Ok(invoice);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetAllInvoices")]
        public async Task<ActionResult<IEnumerable<InvoiceViewModel>>> GetAllInvoices()
        {
            var invoiceList = await _mediator.Send(new GetAllInvoicesQuery());
            return Ok(invoiceList);
        }
    }
}
