using AutoMapper;
using CartingService.Domain.Commands.V1.UpdateCartItem;
using MassTransit;
using MediatR;
using OnlineStore.Contracts;

namespace CartingService.Infrastructure.MessageBroker;

public class ProductConsumer : IConsumer<ProductMessage>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Consume(ConsumeContext<ProductMessage> context)
    {
        var command = _mapper.Map<UpdateCartItemCommand>(context.Message);
        await _mediator.Send(command);
    }
}