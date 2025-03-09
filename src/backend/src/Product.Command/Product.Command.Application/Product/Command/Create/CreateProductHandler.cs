using MediatR;
using FluentValidation;
using AutoMapper;
using Product.Command.Repository;
using BaseInfrastructureMessaging;
using Product.Contracts.Events;

namespace Product.Command.Application.Create
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductCommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMessageBus _bus;

        public CreateProductHandler(IMessageBus bus, IMediator mediator, IProductCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _bus = bus;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (validationResult != null && !validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var record = _mapper.Map<ProductCommandDomainEntities.Product>(command);

            var created = await _repository.SaveAsync(record);
            var result = _mapper.Map<CreateProductResult>(created);

            await _mediator.Publish(new CreateProductResult
            {
                Id = result.Id,
                Price = result.Price,
                Name = record.Name
            });
           
            await _bus.PublishAsync(new ProductCreatedEvent() 
            {
                Id = result.Id,
                Name = record.Name,
                CreatedAt = record.CreatedAt,
                Price = record.Price,
                Status = record.Status.ToString(),
                StatusCommercialization = record.StatusCommercialization.ToString(),
                CommercializedAt = record.CommercializedAt,
                CommercializedCancelledAt = record.CommercializedCancelledAt   
            });

            return result;
        }
    }
}
