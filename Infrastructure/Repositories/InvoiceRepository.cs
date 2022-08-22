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
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> GetInvoiceByIdForCustomerAsync(int customerId, int invoiceId)
        {
            return await _dbContext
                .Set<Invoice>()
                .Where(x => x.Customer.Id == customerId)
                .Include(x => x.Vehicle)
                .Include(x => x.Customer)
                .Include(x => x.RentalCompany)
                .Include(x => x.Reservation)
                .FirstOrDefaultAsync(x => x.Id == invoiceId);
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesForCustomerAsync(int customerId)
        {
            return await _dbContext
                .Set<Invoice>()
                .Where(x => x.Customer.Id == customerId)
                .Include(x => x.Vehicle)
                .Include(x => x.Customer)
                .Include(x => x.Reservation)
                .Include(x => x.RentalCompany)
                .ToListAsync();
        }
    }
}
