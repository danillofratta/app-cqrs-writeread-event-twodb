using Base.Core.Domain.Service;
using FluentValidation;
using Product.Command.Domain.Specification;
using Product.Command.Domain.Validation;
using Product.Command.Repository;

namespace Product.Command.Domain.Service;

public class ProductCancelledService : BaseService
{
    private readonly IProductCommandRepository _commandrepository;
    private readonly IProductQueryRepository _queryrepository;
    private readonly ProductExistsValidator _SaleExistsValidator;

    public ProductCancelledService(IProductCommandRepository commandrepository, IProductQueryRepository queryRepository, ProductExistsValidator saleExistsValidator)
    {
        _commandrepository = commandrepository;
        _queryrepository = queryRepository;
        _SaleExistsValidator = saleExistsValidator;
    }

    public async Task<bool> Process(Guid idsale)
    {
        var validator = new ProductExistsValidator(_queryrepository);
        var validationResult = await validator.ValidateAsync(new ProductCommandDomainEntities.Product { Id = idsale });
        if (validationResult != null && !validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _queryrepository.GetByIdAsync(idsale);

        var canBeCancelled = new ProductCanBeCancelledSpecification().IsSatisfiedBy(sale);
        if (!canBeCancelled)
            throw new ValidationException("The Product cannot be cancelled at this stage.");

        sale.SetCancelled();
        await _commandrepository.UpdateAsync(sale);

        return true;
    }
}

