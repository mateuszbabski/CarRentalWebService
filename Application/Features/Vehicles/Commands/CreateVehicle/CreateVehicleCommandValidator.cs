using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("Every field must be filled");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Every field must be filled");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Every field must be filled");

            RuleFor(x => x.Color)
                .NotEmpty()
                .WithMessage("Every field must be filled");

            RuleFor(x => x.ProductionYear)
                .NotEmpty()
                .WithMessage("Every field must be filled");

            RuleFor(x => x.FuelType)
                .NotEmpty()
                .WithMessage("Every field must be filled");

            RuleFor(x => x.NumberOfSeats)
                .NotEmpty()
                .WithMessage("Every field must be filled");

            RuleFor(x => x.DailyCost)
                .NotEmpty()
                .WithMessage("Every field must be filled");
        }
    }
}
