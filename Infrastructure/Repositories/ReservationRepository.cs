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
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsForCustomerIdAsync(int customerId)
        {
            return await _dbContext
                .Set<Reservation>()
                .Where(x => x.Customer.Id == customerId)
                .Include(x => x.Vehicle)
                .Include(x => x.Customer)
                .Include(x => x.RentalCompany)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdForCustomerAsync(int customerId, int reservationId)
        {
            return await _dbContext
                .Set<Reservation>()
                .Where(x => x.Customer.Id == customerId)
                .Include(x => x.Vehicle)
                .Include(x => x.Customer)
                .Include(x => x.RentalCompany)
                .FirstOrDefaultAsync(x => x.Id == reservationId);
        }
        public async Task<IEnumerable<Reservation>> GetAllReservationsForCompanyIdAsync(int companyId)
        {
            return await _dbContext
                .Set<Reservation>()
                .Where(x => x.RentalCompany.Id == companyId)
                .Include(x => x.Vehicle)
                .Include(x => x.Customer)
                .Include(x => x.RentalCompany)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdForCompanyAsync(int companyId, int reservationId)
        {
            return await _dbContext
                .Set<Reservation>()
                .Where(x => x.RentalCompany.Id == companyId)
                .Include(x => x.Vehicle)
                .Include(x => x.Customer)
                .Include(x => x.RentalCompany)
                .FirstOrDefaultAsync(x => x.Id == reservationId);
        }
    }
}
