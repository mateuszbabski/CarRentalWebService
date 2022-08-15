using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdQuery : IRequest<VehicleViewModel>
    {
        public int Id { get; set; }
    }
}
