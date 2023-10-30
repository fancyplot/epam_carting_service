using CartingService.Domain.Interfaces.V2;
using CartingService.Domain.Models.V2;
using MediatR;

namespace CartingService.Domain.Queries.V2.GetCarts;

public class GetCartsHandler : IRequestHandler<GetCartsQuery, IEnumerable<CartItem>>
{
    private readonly ICartsRepository _cartsRepository;

    public GetCartsHandler(ICartsRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task<IEnumerable<CartItem>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
    {
        return await _cartsRepository.GetCartItemsAsync(request.CartId, cancellationToken);
    }
}