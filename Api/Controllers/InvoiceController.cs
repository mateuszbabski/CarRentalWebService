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
using Application.Features.Invoices.Queries.GetAllInvoicesListAsCompany;
using Application.Features.Invoices.Queries.GetInvoiceByIdAsCompany;
using Application.Features.Vehicles.Commands.DeleteVehicle;
using Application.Features.Invoices.Commands.DeleteInvoice;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddInvoice([FromBody] CreateInvoiceCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetAllInvoices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<InvoiceViewModel>>> GetAllInvoices()
        {
            var invoiceList = await _mediator.Send(new GetAllInvoicesQuery());
            return Ok(invoiceList);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{id}", Name = "GetInvoicenById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InvoiceViewModel>> GetInvoiceById(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery()
            {
                Id = id
            });

            return Ok(invoice);
        }


        [Authorize(Roles = "Company")]
        [HttpGet("GetAllInvoicesAsCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<InvoiceViewModel>>> GetAllInvoicesAsCompany()
        {
            var invoiceList = await _mediator.Send(new GetAllInvoicesAsCompanyQuery());
            return Ok(invoiceList);
        }

        [Authorize(Roles = "Company")]
        [HttpGet("GetInvoiceByIdAsCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InvoiceViewModel>> GetInvoiceByIdAsCompany(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdAsCompanyQuery()
            {
                Id = id
            });

            return Ok(invoice);
        }

        [Authorize(Roles = "Company")]
        [HttpDelete("{id}", Name = "DeleteInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteInvoice(int id)
        {
            var deleteInvoiceCommand = new DeleteInvoiceCommand()
            {
                Id = id
            };
            await _mediator.Send(deleteInvoiceCommand);
            return NoContent();
        }
    }
}
