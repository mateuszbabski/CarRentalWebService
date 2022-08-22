using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoice
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public decimal FullPriceForRenting { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyIdentificationNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string FuelType { get; set; }
        public int ProductionYear { get; set; }
        public bool IsPaid { get; set; }
    }
}



