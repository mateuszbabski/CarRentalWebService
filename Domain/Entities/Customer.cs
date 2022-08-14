using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string DriverLicence { get; set; }

        public Roles Role { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public virtual List<Reservation> ReservationsHistory { get; set; }
        public virtual List<Invoice> InvoicesHistory { get; set; }
    }
}
