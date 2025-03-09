using MediatR;
using FluentValidation;
using AutoMapper;
using Product.Command.Repository;
using BaseInfrastructureMessaging;
using Product.Contracts.Events;

namespace Product.Command.Application.Delete
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IProductCommandRepository _commandrepository;
        private readonly IProductQueryRepository _queryrepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMessageBus _bus;

        public DeleteProductHandler(IMessageBus bus, IProductCommandRepository commandrepository, IProductQueryRepository queryrepository, IMapper mapper, IMediator mediator)
        {
            _queryrepository = queryrepository;
            _commandrepository = commandrepository;
            _mapper = mapper;
            _mediator = mediator;
            _bus = bus;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductCommandValidator(_queryrepository);
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (validationResult != null && !validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var record = await _queryrepository.GetByIdAsync(command.Id);
            var update = await _commandrepository.DeleteAsync(record);

            await _bus.PublishAsync(new ProductDeletedEvent()
            {
                Id = command.Id               
            });

            return _mapper.Map<DeleteProductResult>(update); ;
        }
    }
}
