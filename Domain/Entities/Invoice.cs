using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public virtual Customer CustomerId { get; set; }
        public virtual RentalCompany RentalCompanyId { get; set; }
        public virtual Reservation ReservationId { get; set; }
        public virtual Vehicle VehicleId { get; set; }

        public decimal FullPriceForRenting { get; set; }
        public bool IsPaid { get; set; }
    }
}
