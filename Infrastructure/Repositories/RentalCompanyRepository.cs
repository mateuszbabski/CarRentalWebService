using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RentalCompanyRepository : IRentalCompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RentalCompanyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RentalCompany> RegisterNewRentalCompanyAsync(RentalCompany company)
        {
            await _dbContext.RentalCompanies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return company;
        }

        public async Task<RentalCompany> GetRentalCompanyByEmailAsync(string email)
        {
            return await _dbContext.RentalCompanies.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateRentalCompanyAsync(RentalCompany company)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RentalCompany> GetRentalCompanyByIdAsync(int id)
        {
            return await _dbContext.RentalCompanies.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
