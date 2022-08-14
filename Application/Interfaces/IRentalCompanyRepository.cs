using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRentalCompanyRepository
    {
        Task<RentalCompany> RegisterNewRentalCompanyAsync(RentalCompany company);
        Task<RentalCompany> GetRentalCompanyByEmailAsync(string email);
        Task<RentalCompany> GetRentalCompanyByIdAsync(int id);
        Task UpdateRentalCompanyAsync(RentalCompany company);
    }
}
