using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<RentalCompany> RentalCompanies { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }
        Task<int> SaveChangesAsync();
    }
}