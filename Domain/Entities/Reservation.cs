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
        public string Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual RentalCompany RentalCompany { get; set; }
        public virtual Vehicle Vehicle { get; set; }

    }
}
