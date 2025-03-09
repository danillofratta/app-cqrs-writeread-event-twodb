using MediatR;

namespace Product.Command.Application.Modify
{
    public class ModifyProductCommand : IRequest<ModifyProductResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
