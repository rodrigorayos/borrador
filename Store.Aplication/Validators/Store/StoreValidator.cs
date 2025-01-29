using FluentValidation;
using Store.Domain.Dtos;

namespace Store.Application.Validators.Store;

public class StoreValidator : AbstractValidator<StoreDto>
{
    public StoreValidator()
    {
        RuleFor(store => store.Name)
            .NotEmpty().WithMessage("El nombre de la tienda es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(store => store.Ubication)
            .NotEmpty().WithMessage("La ubicación de la tienda es requerida.")
            .MaximumLength(200).WithMessage("La ubicación no puede exceder los 200 caracteres.");

        RuleFor(store => store.Capacity)
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor que 0.");
    }
}