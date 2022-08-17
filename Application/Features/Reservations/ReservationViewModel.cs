using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations
{
    public class ReservationViewModel
    {
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public string Status { get; set; }
        public decimal DailyCost { get; set; }

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public string CompanyName { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int NumberOfSeats { get; set; }
        public string FuelType { get; set; }
        public int ProductionYear { get; set; }
        public string Color { get; set; }

    }
}
