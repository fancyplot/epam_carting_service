using CartingService.Domain.Interfaces.V1;
using MediatR;

namespace CartingService.Domain.Commands.V1.UpdateCartItem;

public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand>
{
    private readonly ICartsRepository _cartsRepository;

    public UpdateCartItemHandler(ICartsRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var existingItems = await _cartsRepository.GetCartItemsAsync(request.Id, cancellationToken);

        foreach (var item in existingItems)
        {
            await _cartsRepository.UpdateCartItemAsync(item.Id, request.Id, request.Image, request.Name, request.Price, cancellationToken);
        }
    }
}