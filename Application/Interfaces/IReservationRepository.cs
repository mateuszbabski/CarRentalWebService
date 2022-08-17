using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllReservationsForCustomerIdAsync(int customerId);
        Task<Reservation> GetReservationByIdForCustomerAsync(int customerId, int reservationId);
    }
}
