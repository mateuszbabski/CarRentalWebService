using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservations.Commands.UpdateReservation
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x => x.ReservationStart)
                .NotEmpty()
                .GreaterThan(DateTime.Now.AddDays(1))
                .WithMessage("Can not make reservation for today or day in the past");

            RuleFor(x => x.ReservationEnd)
                .NotEmpty()
                .GreaterThan(a => a.ReservationStart.AddDays(1))
                .WithMessage("Can not make reservation for least than one day");
        }
    }
}
