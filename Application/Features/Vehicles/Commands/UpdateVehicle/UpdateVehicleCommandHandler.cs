using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;

        public UpdateVehicleCommandHandler(IVehicleRepository repository, IMapper mapper, ICurrentUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var companyId = _userService.UserId;
            var vehicle = await _repository.GetByIdAsync(request.Id);
            if (vehicle == null || companyId != vehicle.CreatedById)
                throw new NotFoundException();

            vehicle.Color = request.Color;
            vehicle.DailyCost = request.DailyCost;
            vehicle.IsAvailable = request.IsAvailable;

            var vehicleDto = _mapper.Map<Vehicle>(vehicle);

            await _repository.UpdateAsync(vehicleDto);
            
            return Unit.Value;
        }
    }
}
