using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Product.Contracts.Events;
using Product.ReadEvent.Consumer.Service;
using Product.ReadEvent.Domain.Repository;
using Product.ReadEvent.Infrastructure.SignalR;

namespace Product.ReadEvent.Consumer.EventeHandlers
{
    public class ProductCreatedEventHandler : IConsumer<ProductCreatedEvent>
    {
        private readonly IProductReadEventCommandRepository _repository;
        private readonly GetListProductsHub _hub;

        public ProductCreatedEventHandler( IProductReadEventCommandRepository repository, GetListProductsHub hub)
        {
            _repository = repository;
            _hub = hub;
        }

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var @event = context.Message;
            var product = new ProductQueryDomainEntities.Product 
            { 
                Id = @event.Id, 
                Name = @event.Name,
                Price = @event.Price,

                CreatedAt = DateTime.UtcNow,

                CommercializedAt = @event.CommercializedAt,
                CommercializedCancelledAt = @event.CommercializedCancelledAt,   

                StatusCommercialization = @event.StatusCommercialization,
                Status = @event.Status
            };

            await _repository.SaveAsync(product);

            await _hub.Execute();
        }
    }
}
