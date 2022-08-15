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
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Company")]
        [HttpPost("CreateVehicle")]
        public async Task<ActionResult<int>> AddVehicle([FromBody]CreateVehicleCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "Company")]
        [HttpPut("{id}", Name = "UpdateVehicle")]
        public async Task<ActionResult> UpdateVehicle([FromBody] UpdateVehicleCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Company")]
        [HttpDelete("{id}", Name = "DeleteVehicle")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var deleteVehicleCommand = new DeleteVehicleCommand()
            {
                Id = id
            };
            await _mediator.Send(deleteVehicleCommand);
            return NoContent();
        }
    }
}
