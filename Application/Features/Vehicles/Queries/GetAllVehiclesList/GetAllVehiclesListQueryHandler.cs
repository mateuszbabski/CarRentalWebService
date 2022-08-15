using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetAllVehiclesList
{
    public class GetAllVehiclesListQueryHandler : IRequestHandler<GetAllVehiclesListQuery, IEnumerable<VehicleViewModel>>
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVehiclesListQueryHandler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VehicleViewModel>> Handle(GetAllVehiclesListQuery request, CancellationToken cancellationToken)
        {
            var vehiclesList = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<VehicleViewModel>>(vehiclesList);
        }
    }
}
