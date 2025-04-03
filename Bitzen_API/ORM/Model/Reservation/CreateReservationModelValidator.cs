using FluentValidation;

namespace Bitzen_API.ORM.Model.Reservation
{
    public class CreateReservationModelValidator : AbstractValidator<CreateReservationModel>
    {
        public CreateReservationModelValidator()
        {            

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("A data/hora de início é obrigatória.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("A data/hora de término é obrigatória.")
                .GreaterThan(x => x.StartTime).WithMessage("A data/hora final deve ser posterior à inicial.");

            RuleFor(x => x)
                .Must(x => x.StartTime.Date == x.EndTime.Date)
                .WithMessage("A reserva deve iniciar e terminar no mesmo dia.");
        }
    }
}
