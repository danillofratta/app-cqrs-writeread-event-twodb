using FluentValidation;
using Product.Command.Repository;

namespace Product.Command.Domain.Validation;

public class ProductExistsValidator : AbstractValidator<ProductCommandDomainEntities.Product>
{
    private readonly IProductQueryRepository _repository;

    public ProductExistsValidator(IProductQueryRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product is required")
            .MustAsync(ExistInDatabase).WithMessage("Product not found");          
    }

    private async Task<bool> ExistInDatabase(Guid id, CancellationToken cancellationToken)
    {
        var record = await _repository.GetByIdAsync(id);
        if (record != null) { return true; } else { return false; }
    }
}