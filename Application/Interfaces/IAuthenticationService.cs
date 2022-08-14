using Application.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> LoginAsync(AuthenticateUserRequest request);
        Task<AuthenticationResponse> RegisterCustomerAsync(RegisterCustomerRequest request);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<ForgotPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request, string token);

    }
}
