using MediatR;

namespace Product.Query.Application.GetById
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}
