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
        private readonly ICurrentUserService _userService;

        public CreateVehicleCommandHandler(IVehicleRepository repository, IMapper mapper, ICurrentUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var companyId = _userService.UserId;
            var validator = new CreateVehicleCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException();

            var vehicle = _mapper.Map<Vehicle>(request);
            vehicle.RentalCompanyId = companyId;

            vehicle = await _repository.AddAsync(vehicle);

            return vehicle.Id;
        }
    }
}
