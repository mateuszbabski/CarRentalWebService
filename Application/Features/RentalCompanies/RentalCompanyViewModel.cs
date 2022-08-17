using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RentalCompanies
{
    public class RentalCompanyViewModel
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
        public List<Vehicle> VehicleList { get; set; }
    }
}
