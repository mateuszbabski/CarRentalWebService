using Application.Exceptions;
using Application.Features.RentalCompany;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RentalCompanies.Queries.GetRentalCompanyById
{
    public class GetRentalCompanyByIdQueryHandler : IRequestHandler<GetRentalCompanyByIdQuery, RentalCompanyViewModel>
    {
        private readonly IRentalCompanyRepository _repository;
        private readonly IMapper _mapper;

        public GetRentalCompanyByIdQueryHandler(IRentalCompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RentalCompanyViewModel> Handle(GetRentalCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var rentalCompany = await _repository.GetRentalCompanyByIdAsync(request.Id);
            if (rentalCompany == null)
                throw new NotFoundException();

            return _mapper.Map<RentalCompanyViewModel>(rentalCompany);
        }
    }
}
