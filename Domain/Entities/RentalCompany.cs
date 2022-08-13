using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RentalCompany : BaseEntity
    {
        public string CompanyName { get; set; }
        public string CompanyIdentificationNumber { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public virtual List<Vehicle> VehicleList { get; set; }
        public virtual List<Invoice> InvoicesHistory { get; set; }
    }
}
