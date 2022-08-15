using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public decimal DailyCost { get; set; }
    }
}
