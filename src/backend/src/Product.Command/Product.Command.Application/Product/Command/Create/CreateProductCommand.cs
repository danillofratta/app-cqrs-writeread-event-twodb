using MediatR;

namespace Product.Command.Application.Create
{
    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
