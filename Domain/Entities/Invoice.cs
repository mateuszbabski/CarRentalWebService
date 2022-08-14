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
        public virtual Customer Customer { get; set; }
        public virtual RentalCompany RentalCompany { get; set; }
        public virtual Reservation Reservation { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public decimal FullPriceForRenting { get; set; }
        public bool IsPaid { get; set; }
    }
}
