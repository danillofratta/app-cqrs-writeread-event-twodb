using MassTransit;
using Product.Contracts.Events;
using Product.ReadEvent.Consumer.Service;
using Product.ReadEvent.Domain.Repository;
using Product.ReadEvent.Infrastructure.Repository;

namespace Product.ReadEvent.Consumer.EventeHandlers
{
    public class ProductUpdatedEventHandler : IConsumer<ProductUpdatedEvent>
    {
        private readonly IProductReadEventCommandRepository _commandrepository;
        private readonly IProductReadEventQueryRepository _queryrepository;
        private readonly GetListProductsHub _hub;

        public ProductUpdatedEventHandler(IProductReadEventCommandRepository commandrepository, IProductReadEventQueryRepository queryrepository, GetListProductsHub hub)
        {
            _queryrepository = queryrepository;
            _commandrepository = commandrepository;
            _hub = hub;
        }


        public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            var @event = context.Message;

            var product = await _queryrepository.GetByIdAsync(@event.Id);

            if (product != null)
            {
                product.Name = @event.Name;
                product.Price = @event.Price;

                product.UpdatedAt = DateTime.UtcNow;

                product.CommercializedAt = @event.CommercializedAt;
                product.CommercializedCancelledAt = @event.CommercializedCancelledAt;

                product.StatusCommercialization = @event.StatusCommercialization;
                product.Status = @event.Status;

                await _commandrepository.UpdateAsync(product);


                await _hub.Execute();
            }
        }
    }
}
