using MediatR;
using FluentValidation;
using AutoMapper;
using Product.Command.Repository;
using Product.Contracts.Events;
using BaseInfrastructureMessaging;

namespace Product.Command.Application.Modify
{
    public class ModifyProductHandler : IRequestHandler<ModifyProductCommand, ModifyProductResult>
    {
        private readonly IProductCommandRepository _commandrepository;
        private readonly IProductQueryRepository _queryrepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMessageBus _bus;

        public ModifyProductHandler(IMessageBus bus, IMediator mediator, IProductCommandRepository commandrepository, IProductQueryRepository productQueryRepository, IMapper mapper)
        {
            _commandrepository = commandrepository; ;
            _queryrepository = productQueryRepository;
            _mapper = mapper;
            _mediator = mediator;
            _bus = bus;
        }

        public async Task<ModifyProductResult> Handle(ModifyProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new ModifyProductCommandValidator(_queryrepository);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (validationResult != null && !validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var record = await _queryrepository.GetByIdAsync(command.Id);//_mapper.Map <ProductCommandDomainEntities.Product>(command);
            record.Price = command.Price;
            record.Name = command.Name;

            var update = await _commandrepository.UpdateAsync(record);
            var result = _mapper.Map<ModifyProductResult>(update);

            await _bus.PublishAsync(new ProductUpdatedEvent()
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
