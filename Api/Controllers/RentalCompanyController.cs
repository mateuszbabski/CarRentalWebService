using Application.Authentication;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalCompanyController : ControllerBase
    {
        private readonly ICompanyAuthService _companyAuthService;

        public RentalCompanyController(ICompanyAuthService companyAuthService)
        {
            _companyAuthService = companyAuthService;
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
    }
}
