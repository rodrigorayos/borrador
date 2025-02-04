using FluentValidation;
using Store.Domain.Dtos.Store;

namespace Store.Application.Validators.Authorization
{
    public class AuthorizationValidation : AbstractValidator<AuthorizationQueryDto>
    {
        public AuthorizationValidation()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("La fecha no puede estar vacía.");

            RuleFor(x => x.Description)
                .MaximumLength(300).WithMessage("La descripción no puede superar los 300 caracteres.");
        }
    }
}