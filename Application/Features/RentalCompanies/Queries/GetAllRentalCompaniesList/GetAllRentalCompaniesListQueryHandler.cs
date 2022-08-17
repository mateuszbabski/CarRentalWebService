using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RentalCompanies.Queries.GetAllRentalCompaniesList
{
    public class GetAllRentalCompaniesListQueryHandler : IRequestHandler<GetAllRentalCompaniesListQuery, IEnumerable<RentalCompanyViewModel>>
    {
        private readonly IRentalCompanyRepository _rentalCompanyRepository;
        private readonly IMapper _mapper;

        public GetAllRentalCompaniesListQueryHandler(IRentalCompanyRepository rentalCompanyRepository, IMapper mapper)
        {
            _rentalCompanyRepository = rentalCompanyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RentalCompanyViewModel>> Handle(GetAllRentalCompaniesListQuery request, CancellationToken cancellationToken)
        {
            var rentalCompaniesList = await _rentalCompanyRepository.GetAllRentalCompaniesListAsync();

            return _mapper.Map<IEnumerable<RentalCompanyViewModel>>(rentalCompaniesList);
        }
    }
}
