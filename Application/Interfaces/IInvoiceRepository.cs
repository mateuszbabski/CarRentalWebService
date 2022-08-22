using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<Invoice> GetInvoiceByIdForCustomerAsync(int customerId, int invoiceId);
        Task<IEnumerable<Invoice>> GetAllInvoicesForCustomerAsync(int customerId);
        Task<Invoice> GetInvoiceByIdForCompanyAsync(int companyId, int invoiceId);
        Task<IEnumerable<Invoice>> GetAllInvoicesForCompanyAsync(int companyId);
    }
}
