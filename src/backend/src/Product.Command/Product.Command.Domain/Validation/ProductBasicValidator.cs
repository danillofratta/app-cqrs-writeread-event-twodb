using FluentValidation;

namespace Product.Command.Domain.Validation;

/// <summary>
/// Performs basic entity validations
/// </summary>
public class SaleBasicValidator : AbstractValidator<ProductCommandDomainEntities.Product>
{
    public SaleBasicValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(s => s.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
