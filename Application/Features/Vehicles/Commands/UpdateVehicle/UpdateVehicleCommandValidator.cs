using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(m => m.Color)
                .NotEmpty()
                .WithMessage("Field can not be empty");

            RuleFor(m => m.DailyCost)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Daily cost can not be less than 0");
        }
    }
}
