using FluentValidation;
using Store.Domain.Models.Authorization;

namespace Store.Application.Validators.Authorization;

public class AuthorizationValidation : AbstractValidator<AuthorizationModel>
{
    public AuthorizationValidation()
    {
        // Validación para la propiedad 'Date'
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("La fecha no puede estar vacía.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

        // Validación para la propiedad 'State'
        RuleFor(x => x.State)
            .NotNull().WithMessage("El estado no puede ser nulo.");

        // Validación para la propiedad 'Description'
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción no puede estar vacía.")
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
    }
}