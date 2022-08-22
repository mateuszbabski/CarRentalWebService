using Application.Features.Reservations;
using Application.Features.Reservations.Commands.CreateReservation;
using Application.Features.Reservations.Commands.DeleteReservation;
using Application.Features.Reservations.Commands.UpdateReservation;
using Application.Features.Reservations.Queries.GetAllReservationListAsCompany;
using Application.Features.Reservations.Queries.GetAllReservationsList;
using Application.Features.Reservations.Queries.GetReservationById;
using Application.Features.Reservations.Queries.GetReservationByIdAsCompany;
using Application.Features.Vehicles;
using Application.Features.Vehicles.Commands.CreateVehicle;
using Application.Features.Vehicles.Commands.DeleteVehicle;
using Application.Features.Vehicles.Commands.UpdateVehicle;
using Application.Features.Vehicles.Queries.GetAllVehiclesList;
using Application.Features.Vehicles.Queries.GetVehicleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("Make-reservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReservationViewModel>> AddReservation([FromBody] CreateReservationCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}", Name = "DeleteReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var deleteReservationCommand = new DeleteReservationCommand()
            {
                Id = id
            };
            await _mediator.Send(deleteReservationCommand);
            return NoContent();
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}", Name = "UpdateReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateReservation([FromBody] UpdateReservationCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetAllReservationsForCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> GetAll()
        {
            var reservationList = await _mediator.Send(new GetAllReservationsListQuery());
            return Ok(reservationList);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetReservationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReservationViewModel>> GetById(int id)
        {
            var reservation = await _mediator.Send(new GetReservationByIdQuery()
            {
                Id = id
            });

            return Ok(reservation);
        }

        [Authorize(Roles = "Company")]
        [Authorize(Roles = "Company")]
        [HttpGet("GetAllReservationsAsCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReservationViewModel>>> GetAllAsCompany()
        {
            var reservationList = await _mediator.Send(new GetAllReservationListAsCompanyQuery());
            return Ok(reservationList);
        }

        [HttpGet("GetReservationByIdAsCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReservationViewModel>> GetByIdAsCompany(int id)
        {
            var reservation = await _mediator.Send(new GetReservationByIdAsCompanyQuery()
            { 
                Id = id
            });
            return Ok(reservation);
        }

    } 
}
