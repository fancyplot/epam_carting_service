using CartingService.Domain.Interfaces.V1;
using MediatR;

namespace CartingService.Domain.Commands.V1.DeleteCart;

public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand>
{
    private readonly ICartsRepository _cartsRepository;

    public DeleteCartItemHandler(ICartsRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var existingCart = await _cartsRepository.GetCartItemAsync(request.CartId, request.Id, cancellationToken);
        if (existingCart == null)
            throw new KeyNotFoundException($"Cart item with id {request.Id} does not exist in cart {request.CartId}");

        await _cartsRepository.DeleteAsync(request.CartId, request.Id, cancellationToken);

        var deletedCart = await _cartsRepository.GetCartItemAsync(request.CartId, request.Id, cancellationToken);
        if (deletedCart != null)
            throw new Exception($"Cart item with id {request.Id} was not deleted in cart {request.CartId}");
    }
}