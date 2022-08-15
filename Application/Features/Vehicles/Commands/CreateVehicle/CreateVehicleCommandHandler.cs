using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;

        public CreateVehicleCommandHandler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateVehicleCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException();

            var vehicle = _mapper.Map<Vehicle>(request);
            vehicle = await _repository.AddAsync(vehicle);

            return vehicle.Id;
        }
    }
}
