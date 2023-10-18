using CartingService.Domain.Interfaces.V1;
using MediatR;

namespace CartingService.Domain.Commands.V1.DeleteCart;

public class DeleteCartHandler : IRequestHandler<DeleteCartCommand>
{
    private readonly ICartsRepository _cartsRepository;

    public DeleteCartHandler(ICartsRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var existingCart = await _cartsRepository.GetAsync(request.Id, cancellationToken);
        if (existingCart == null)
            throw new KeyNotFoundException($"Cart with id {request.Id} does not exist");

        await _cartsRepository.DeleteAsync(request.Id, cancellationToken);

        var deletedCart = await _cartsRepository.GetAsync(request.Id, cancellationToken);
        if(deletedCart != null)
            throw new Exception($"Cart with id {request.Id} was not deleted");
    }
}