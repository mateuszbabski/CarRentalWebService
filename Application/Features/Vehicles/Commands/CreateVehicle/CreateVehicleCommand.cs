﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<int>
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
        public string FuelType { get; set; }
        public int ProductionYear { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; set; } = true;
        public decimal DailyCost { get; set; }
    }
}
