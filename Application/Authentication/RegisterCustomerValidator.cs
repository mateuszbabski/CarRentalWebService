using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class RegisterCustomerValidator : AbstractValidator<RegisterCustomerRequest>
    {
        public RegisterCustomerValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Incorrect format of email");

            RuleFor(m => m.Password)
                .MinimumLength(6)
                .WithMessage("Password is too short");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .WithMessage("Password and confirm password are not the same");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches("^[0-9]*$")
                .WithMessage("Only digits allowed");

            RuleFor(m => m.FirstName)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.LastName)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.DateOfBirth)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.DriverLicence)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.Country)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.City)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.Street)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.PostalCode)
                .NotEmpty()
                .WithMessage("Field can not be empty");
        }
    }
}
