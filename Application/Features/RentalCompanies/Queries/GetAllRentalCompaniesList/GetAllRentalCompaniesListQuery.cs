using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RentalCompanies.Queries.GetAllRentalCompaniesList
{
    public class GetAllRentalCompaniesListQuery : IRequest<IEnumerable<RentalCompanyViewModel>>
    {
    }
}
