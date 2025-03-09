using MediatR;
using FluentValidation;
using AutoMapper;
using Product.Query.Domain.Repository;

namespace Product.Query.Application.GetByName;

public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, List<GetProductByNameQueryResult>>
{
    private readonly IProductQueryRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByNameQueryHandler(IProductQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetProductByNameQueryResult>> Handle(GetProductByNameQuery command, CancellationToken cancellationToken)
    {
        var validator = new GetProductByNameQueryValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult != null && !validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Product = await _repository.GetByName(command.Name);
        if (Product == null)
            throw new KeyNotFoundException($"Product with ID {command.Name} not found");

        return _mapper.Map<List<GetProductByNameQueryResult>>(Product);
    }
}

