using Application.Authentication;
using Application.Features;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Settings;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class CompanyAuthService : ICompanyAuthService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IRentalCompanyRepository _rentalCompanyRepository;

        public CompanyAuthService(
            JWTSettings jwtSettings,
            IMapper mapper,
            IEmailService emailService,
            IRentalCompanyRepository rentalCompanyRepository)
        {
            _jwtSettings = jwtSettings;
            _mapper = mapper;
            _emailService = emailService;
            _rentalCompanyRepository = rentalCompanyRepository;
        }

        public async Task<AuthenticationResponse> LoginRentalCompanyAsync(AuthenticateUserRequest request)
        {
            var rentalCompany = await _rentalCompanyRepository.GetRentalCompanyByEmailAsync(request.Email);
            if (rentalCompany == null)
                return new AuthenticationResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "Email or password incorrect" }
                };

            if (!VerifyPasswordHash(request.Password, rentalCompany.PasswordHash, rentalCompany.PasswordSalt))
                return new AuthenticationResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "Email or password incorrect" }
                };

            var token = GenerateJwtToken(rentalCompany);

            return new AuthenticationResponse
            {
                IsSuccess = true,
                Id = rentalCompany.Id,
                JWTToken = token
            };
        }

        public async Task<AuthenticationResponse> RegisterRentalCompanyAsync(RegisterRentalCompanyRequest request)
        {
            var isEmailInUse = await _rentalCompanyRepository.GetRentalCompanyByEmailAsync(request.Email);
            if (isEmailInUse != null)
                return new AuthenticationResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "Email is already taken" }
                };

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newCompany = _mapper.Map<RentalCompany>(request);
            newCompany.PasswordHash = passwordHash;
            newCompany.PasswordSalt = passwordSalt;

            await _rentalCompanyRepository.RegisterNewRentalCompanyAsync(newCompany);

            return new AuthenticationResponse
            {
                IsSuccess = true,
                Id = newCompany.Id,
                Email = newCompany.Email
            };
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var rentalCompany = await _rentalCompanyRepository.GetRentalCompanyByEmailAsync(request.Email);
            if (rentalCompany == null)
                return new ForgotPasswordResponse
                {
                    IsSuccess = false,
                    Errors = "Invalid email"
                };

            rentalCompany.PasswordResetToken = CreateRandomToken();
            rentalCompany.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _rentalCompanyRepository.UpdateRentalCompanyAsync(rentalCompany);

            var route = "api/account/reset-password";
            var origin = "https://localhost:";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var passwordResetUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "token", rentalCompany.PasswordResetToken);

            var emailRequest = new EmailRequest()
            {
                Body = $"Reset token - {rentalCompany.PasswordResetToken} - {passwordResetUri}",
                To = request.Email,
                Subject = "Reset Password Token"
            };

            await _emailService.SendAsync(emailRequest);

            return new ForgotPasswordResponse
            {
                IsSuccess = true,
                Errors = null
            };
        }

        public async Task<ForgotPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request, string token)
        {
            var rentalCompany = await _rentalCompanyRepository.GetRentalCompanyByEmailAsync(request.Email);
            if (rentalCompany == null || rentalCompany.PasswordResetToken == null
                             || rentalCompany.PasswordResetToken != token
                             || rentalCompany.ResetTokenExpires < DateTime.Now)
                return new ForgotPasswordResponse
                {
                    IsSuccess = false,
                    Errors = "Invalid email or token"
                };

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            rentalCompany.PasswordHash = passwordHash;
            rentalCompany.PasswordSalt = passwordSalt;
            rentalCompany.PasswordResetToken = null;
            rentalCompany.ResetTokenExpires = null;

            await _rentalCompanyRepository.UpdateRentalCompanyAsync(rentalCompany);
            return new ForgotPasswordResponse
            {
                IsSuccess = true,
                Errors = null,
            };

        }

        private string GenerateJwtToken(RentalCompany rentalCompany)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, rentalCompany.Id.ToString()),
                new Claim(ClaimTypes.Email, $"{rentalCompany.Email}"),
                new Claim(ClaimTypes.Role, $"{rentalCompany.Role}"),
                new Claim(ClaimTypes.Country, $"{rentalCompany.Country}")
            };

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtSettings.DurationInDays);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
