using MediatR;


namespace Product.Command.Application.Modify
{
    public class ModifyProductResult : INotification
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
