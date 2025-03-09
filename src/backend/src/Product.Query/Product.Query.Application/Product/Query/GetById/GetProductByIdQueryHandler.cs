using MediatR;
using FluentValidation;
using AutoMapper;
using Product.Query.Domain.Repository;

namespace Product.Query.Application.GetById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    private readonly IProductQueryRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery command, CancellationToken cancellationToken)
    {
        var validator = new GetProductByIdQueryValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult != null && !validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Product = await _repository.GetByIdAsync(command.Id);
        if (Product == null)
            throw new KeyNotFoundException($"Product with ID {command.Id} not found");

        return _mapper.Map<GetProductByIdQueryResult>(Product);
    }
}

