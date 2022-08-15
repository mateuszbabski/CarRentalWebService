using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetAllVehiclesForCompany
{
    public class GetAllVehiclesForCompanyQuery : IRequest<IEnumerable<VehicleViewModel>>
    {
        public int companyId { get; set; }
    }
}
