using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetAllVehiclesList
{
    public class GetAllVehiclesListQuery : IRequest<IEnumerable<VehicleViewModel>>
    {
    }
}
