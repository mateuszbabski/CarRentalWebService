using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _userService;

        public ApplicationDbContext(DbContextOptions options, ICurrentUserService userService) : base(options)
        {
            _userService = userService;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<RentalCompany> RentalCompanies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                entry.Entity.CreatedById = _userService.UserId;
            }

            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
