using FluentValidation;
using Product.Command.Repository;

namespace Product.Command.Application.Modify
{
    public class ModifyProductCommandValidator : AbstractValidator<ModifyProductCommand>
    {
        private readonly IProductQueryRepository _repository;

        public ModifyProductCommandValidator(IProductQueryRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product is required")
                .MustAsync(ExistInDatabase).WithMessage("Product not found");

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }

        private async Task<bool> ExistInDatabase(Guid id, CancellationToken cancellationToken)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record != null) { return true; } else { return false; }
        }
    }
}
