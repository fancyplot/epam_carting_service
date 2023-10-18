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
        await _cartsRepository.DeleteAsync(request.Id, cancellationToken);
    }
}