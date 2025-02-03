using FluentValidation;
using Store.Domain.Models.Store;

namespace Store.Application.Validators.Store;

public class StoreValidation : AbstractValidator<StoreModel>
{
    public StoreValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre no puede estar vacio")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");
        RuleFor(x => x.Ubication)
            .NotEmpty().WithMessage("La ubicación no puede estar vacia")
            .MaximumLength(300).WithMessage("La ubicaión no puede exceder los 300 caracteres");
        RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("La capcidad no puede estar vacia")
            .GreaterThan(0).WithMessage("La capacidad no puede ser 0");
    }
}