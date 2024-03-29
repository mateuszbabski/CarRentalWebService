﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class RegisterRentalCompanyValidator : AbstractValidator<RegisterRentalCompanyRequest>
    {
        public RegisterRentalCompanyValidator()
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

            RuleFor(m => m.CompanyName)
                .NotEmpty()
                .WithMessage("Name can not be empty");

            RuleFor(m => m.CompanyIdentificationNumber)
                .NotEmpty()
                .WithMessage("Insert correct Identification Number");

            RuleFor(m => m.OwnerFirstName)
                .NotEmpty()
                .WithMessage("Name can not be empty");

            RuleFor(m => m.OwnerLastName)
                .NotEmpty()
                .WithMessage("Name can not be empty");

            RuleFor(m => m.Country)
                .NotEmpty()
                .WithMessage("Insert full address");

            RuleFor(m => m.City)
                .NotEmpty()
                .WithMessage("Insert full address");

            RuleFor(m => m.Street)
                .NotEmpty()
                .WithMessage("Insert full address");

            RuleFor(m => m.PostalCode)
                .NotEmpty()
                .WithMessage("Insert full address");
        }
    }
}
