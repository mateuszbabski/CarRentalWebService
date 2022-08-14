using Application.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICompanyAuthService
    {
        Task<AuthenticationResponse> LoginRentalCompanyAsync(AuthenticateUserRequest request);
        Task<AuthenticationResponse> RegisterRentalCompanyAsync(RegisterRentalCompanyRequest request);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<ForgotPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request, string token);

    }
}
