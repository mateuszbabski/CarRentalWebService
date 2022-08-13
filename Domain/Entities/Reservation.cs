using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public Status Status { get; set; }
        public decimal DailyCost { get; set; }

        public virtual Customer CustomerId { get; set; }
        public virtual RentalCompany RentalCompanyId { get; set; }
        public virtual Vehicle VehicleId { get; set; }

    }
}
