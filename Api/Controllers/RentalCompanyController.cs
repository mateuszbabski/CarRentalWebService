using Application.Authentication;
using Application.Features.RentalCompanies.Queries.GetRentalCompanyById;
using Application.Features.RentalCompanies;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.RentalCompanies.Queries.GetAllRentalCompaniesList;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalCompanyController : ControllerBase
    {
        private readonly ICompanyAuthService _companyAuthService;
        private readonly IMediator _mediator;

        public RentalCompanyController(ICompanyAuthService companyAuthService, IMediator mediator)
        {
            _companyAuthService = companyAuthService;
            _mediator = mediator;
        }

        [HttpPost("authenticate-company")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest request)
        {
            return Ok(await _companyAuthService.LoginRentalCompanyAsync(request));
        }

        [HttpPost("register-rental-company")]
        public async Task<IActionResult> RegisterNewRentalCompanyAsync([FromBody] RegisterRentalCompanyRequest request)
        {
            return Ok(await _companyAuthService.RegisterRentalCompanyAsync(request));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            return Ok(await _companyAuthService.ForgotPasswordAsync(request));
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request, [FromQuery] string token)
        {
            return Ok(await _companyAuthService.ResetPasswordAsync(request, token));
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<ActionResult<RentalCompanyViewModel>> GetRentalCompanyById(int id)
        {
            var rentalCompany = await _mediator.Send(new GetRentalCompanyByIdQuery()
            {
                Id = id
            });
            return Ok(rentalCompany);
        }

        [HttpGet("GetAllCompanies")]
        public async Task<ActionResult<IEnumerable<RentalCompanyViewModel>>> GetAllRentalCompaniesAsync()
        {
            var rentalCompaniesList = await _mediator.Send(new GetAllRentalCompaniesListQuery());
            
            return Ok(rentalCompaniesList);
        }
    }
}
