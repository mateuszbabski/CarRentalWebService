using Application.Features.Reservations;
using Application.Features.Reservations.Commands.CreateReservation;
using Application.Features.Reservations.Commands.DeleteReservation;
using Application.Features.Reservations.Commands.UpdateReservation;
using Application.Features.Vehicles.Commands.CreateVehicle;
using Application.Features.Vehicles.Commands.DeleteVehicle;
using Application.Features.Vehicles.Commands.UpdateVehicle;
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
        public async Task<ActionResult<ReservationViewModel>> AddVehicle([FromBody] CreateReservationCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}", Name = "DeleteReservation")]
        public async Task<ActionResult> DeleteVehicle(int id)
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
        public async Task<ActionResult> UpdateVehicle([FromBody] UpdateReservationCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    } 
}
