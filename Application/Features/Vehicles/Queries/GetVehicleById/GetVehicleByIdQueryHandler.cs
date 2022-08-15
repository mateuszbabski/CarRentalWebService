using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleViewModel>
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VehicleViewModel> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _repository.GetByIdAsync(request.Id);
            if (vehicle == null)
                throw new NotFoundException();

            return _mapper.Map<VehicleViewModel>(vehicle);
        }
    }
}
