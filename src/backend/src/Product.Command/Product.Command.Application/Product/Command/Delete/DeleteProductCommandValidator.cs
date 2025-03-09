using FluentValidation;
using Product.Command.Repository;


namespace Product.Command.Application.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductQueryRepository _repository;
        public DeleteProductCommandValidator(IProductQueryRepository repository)
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
}
