using MassTransit;
using Product.Contracts.Events;
using Product.ReadEvent.Consumer.Service;
using Product.ReadEvent.Domain.Repository;
using Product.ReadEvent.Infrastructure.Repository;

namespace Product.ReadEvent.Consumer.EventeHandlers
{
    public class ProductDeletedEventHandler : IConsumer<ProductDeletedEvent>
    {
        private readonly IProductReadEventCommandRepository _commandrepository;
        private readonly IProductReadEventQueryRepository _queryrepository;
        private readonly GetListProductsHub _hub;

        public ProductDeletedEventHandler(IProductReadEventCommandRepository commandrepository, IProductReadEventQueryRepository queryrepository, GetListProductsHub hub)
        {
            _queryrepository = queryrepository;
            _commandrepository = commandrepository;
            _hub = hub;
        }

        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            var @event = context.Message;
            var product = await _queryrepository.GetByIdAsync(@event.Id);
            if (product != null)
            {
                await _commandrepository.DeleteAsync(product);

                await _hub.Execute();
            }
        }
    }
}
