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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> RegisterNewCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
