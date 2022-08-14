using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> RegisterNewCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerByEmailAsync(string email);
    }
}
