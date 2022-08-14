using Application.Authentication;
using Application.Features;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Settings;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ICustomerRepository _customerRepository;

        public AuthenticationService(
            JWTSettings jwtSettings,
            IMapper mapper,
            ICurrentUserService userService,
            IEmailService emailService,
            ICustomerRepository customerRepository)
        {
            _jwtSettings = jwtSettings;
            _mapper = mapper;
            _userService = userService;
            _emailService = emailService;
            _customerRepository = customerRepository;
        }

        public async Task<AuthenticationResponse> LoginAsync(AuthenticateUserRequest request)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
            if (customer == null)
                return new AuthenticationResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "Email or password incorrect" }
                };

            if (!VerifyPasswordHash(request.Password, customer.PasswordHash, customer.PasswordSalt))
                return new AuthenticationResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "Email or password incorrect" }
                };

            var token = GenerateJwtToken(customer);

            return new AuthenticationResponse
            {
                IsSuccess = true,
                Id = customer.Id,
                JWTToken = token
            };
        }

        public async Task<AuthenticationResponse> RegisterCustomerAsync(RegisterCustomerRequest request)
        {
            var isEmailInUse = await _customerRepository.GetCustomerByEmailAsync(request.Email);
            if (isEmailInUse != null)
                return new AuthenticationResponse
                {
                    IsSuccess = false,
                    Errors = new[] { "Email is already taken" }
                };

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newCustomer = _mapper.Map<Customer>(request);
            await _customerRepository.RegisterNewCustomerAsync(newCustomer);

            return new AuthenticationResponse
            {
                IsSuccess = true,
                Id = newCustomer.Id
            };
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
            if (customer == null)
                return new ForgotPasswordResponse
                {
                    IsSuccess = false,
                    Errors = "Invalid email"
                };

            customer.PasswordResetToken = CreateRandomToken();
            customer.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _customerRepository.UpdateCustomerAsync(customer);

            var route = "api/customer/reset-password";
            var origin = "https://localhost:";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var passwordResetUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "token", customer.PasswordResetToken);

            var emailRequest = new EmailRequest()
            {
                Body = $"Reset token - {customer.PasswordResetToken} - {passwordResetUri}",
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
            var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
            if (customer == null || customer.PasswordResetToken == null
                             || customer.PasswordResetToken != token
                             || customer.ResetTokenExpires < DateTime.Now)
                return new ForgotPasswordResponse
                {
                    IsSuccess = false,
                    Errors = "Invalid email or token"
                };

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            customer.PasswordResetToken = null;
            customer.ResetTokenExpires = null;
            
            await _customerRepository.UpdateCustomerAsync(customer);
            return new ForgotPasswordResponse
            {
                IsSuccess = true,
                Errors = null,
            };

        }

        private string GenerateJwtToken(Customer customer)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Email, $"{customer.Email}"),
                new Claim(ClaimTypes.Role, $"{customer.Role}"),
                new Claim(ClaimTypes.Country, $"{customer.FirstName}")
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
