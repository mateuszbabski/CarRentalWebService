using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetAllVehiclesForCompany
{
    public class GetAllVehiclesForCompanyQueryHandler : IRequestHandler<GetAllVehiclesForCompanyQuery, IEnumerable<VehicleViewModel>>
    {
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVehiclesForCompanyQueryHandler(IVehicleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VehicleViewModel>> Handle(GetAllVehiclesForCompanyQuery request, CancellationToken cancellationToken)
        {
            var vehiclesList = await _repository.GetAllVehiclesForCompanyAsync(request.companyId);
            if (vehiclesList == null)
                throw new NotFoundException();

            return _mapper.Map<IEnumerable<VehicleViewModel>>(vehiclesList);
        }
    }
}
