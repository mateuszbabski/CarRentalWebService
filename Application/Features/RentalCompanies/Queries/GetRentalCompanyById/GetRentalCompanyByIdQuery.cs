using Application.Features.RentalCompany;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RentalCompanies.Queries.GetRentalCompanyById
{
    public class GetRentalCompanyByIdQuery : IRequest<RentalCompanyViewModel>
    {
        public int Id { get; set; }
    }
}
