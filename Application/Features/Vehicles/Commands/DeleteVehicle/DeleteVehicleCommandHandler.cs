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

namespace Application.Features.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly IVehicleRepository _repository;
        private readonly ICurrentUserService _userService;

        public DeleteVehicleCommandHandler(IVehicleRepository repository, ICurrentUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var companyId = _userService.UserId;
            var vehicle = await _repository.GetByIdAsync(request.Id);
            if (vehicle == null || companyId != vehicle.CreatedById)
                throw new NotFoundException();
            await _repository.DeleteAsync(vehicle);

            return Unit.Value;

        }
    }
}
