using Domain.Common;
using Domain.Enums;
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
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Roles Role { get; set; } = Roles.Company;
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public List<Vehicle> VehicleList { get; set; }
        public virtual List<Invoice> InvoicesHistory { get; set; }
        public virtual List<Reservation> ReservationList { get; set; }
        //public virtual List<Customer> CustomerList { get; set; }
    }
}
