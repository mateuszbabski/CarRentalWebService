using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
        public string FuelType { get; set; }
        public int ProductionYear { get; set; }
        public string Color { get; set; }
        public bool IsAvailable { get; set; }
        public decimal DailyCost { get; set; }

        //public RentalCompany RentalCompanyName { get; set; }
    }
}
