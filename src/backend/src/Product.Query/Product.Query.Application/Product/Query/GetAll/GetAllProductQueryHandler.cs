using AutoMapper;
using MediatR;
using Product.Query.Application.GetById;
using Product.Query.Domain.Repository;

namespace Product.Query.Application.GetAll;
public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<GetAllProductQueryResult>>
{
    private readonly IProductQueryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IProductQueryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetAllProductQueryResult>> Handle(GetAllProductQuery command, CancellationToken cancellationToken)
    {
        var Product = await _repository.GetAllAsync();
       
        return _mapper.Map<List<GetAllProductQueryResult>>(Product);
    }
}


