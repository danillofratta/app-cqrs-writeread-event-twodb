using MediatR;

namespace Product.Query.Application.GetByName
{
    public class GetProductByNameQuery : IRequest<List<GetProductByNameQueryResult>>
    {
        public string Name { get; set; } = string.Empty;

        public GetProductByNameQuery(string name)
        {
            Name = name;
        }

    }
}
