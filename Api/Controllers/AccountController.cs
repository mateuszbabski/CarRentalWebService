using Application.Authentication;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest request)
        {
            return Ok(await _authenticationService.LoginCustomerAsync(request));
        }

        [HttpPost("register-customer")]
        public async Task<IActionResult> RegisterNewCustomerAsync([FromBody] RegisterCustomerRequest request)
        {
            return Ok(await _authenticationService.RegisterCustomerAsync(request));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            return Ok(await _authenticationService.ForgotPasswordAsync(request));
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request, [FromQuery]string token)
        {
            return Ok(await _authenticationService.ResetPasswordAsync(request, token));
        }
    }
}
