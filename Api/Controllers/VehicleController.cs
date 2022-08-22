using Application.Features.Vehicles;
using Application.Features.Vehicles.Commands.CreateVehicle;
using Application.Features.Vehicles.Commands.DeleteVehicle;
using Application.Features.Vehicles.Commands.UpdateVehicle;
using Application.Features.Vehicles.Queries.GetAllVehiclesForCompany;
using Application.Features.Vehicles.Queries.GetAllVehiclesList;
using Application.Features.Vehicles.Queries.GetVehicleById;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> AddVehicle([FromBody]CreateVehicleCommand request)
        {
            return await _mediator.Send(request);
        }

        [Authorize(Roles = "Company")]
        [HttpPut("{id}", Name = "UpdateVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateVehicle([FromBody] UpdateVehicleCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = "Company")]
        [HttpDelete("{id}", Name = "DeleteVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var deleteVehicleCommand = new DeleteVehicleCommand()
            {
                Id = id
            };
            await _mediator.Send(deleteVehicleCommand);
            return NoContent();
        }

        [HttpGet("GetAllVehicles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<VehicleViewModel>>> GetAll()
        {
            var vehiclesList = await _mediator.Send(new GetAllVehiclesListQuery());
            return Ok(vehiclesList);
        }

        [HttpGet("{id}", Name = "GetVehicleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VehicleViewModel>> GetById(int id)
        {
            var vehicle = await _mediator.Send(new GetVehicleByIdQuery()
            {
                Id = id
            });

            return Ok(vehicle);
        }

        [HttpGet("GetVehicleByCompanyId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<VehicleViewModel>>> GetByCompanyId(int id)
        {
            var vehicle = await _mediator.Send(new GetAllVehiclesForCompanyQuery()
            {
                companyId = id
            });

            return Ok(vehicle);
        }
    }
}
