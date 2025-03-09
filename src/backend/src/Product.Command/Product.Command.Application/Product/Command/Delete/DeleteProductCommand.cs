using MediatR;

namespace Product.Command.Application.Delete
{
    public class DeleteProductCommand : IRequest<DeleteProductResult>
    {
        public Guid Id { get; set; }
    }
}
