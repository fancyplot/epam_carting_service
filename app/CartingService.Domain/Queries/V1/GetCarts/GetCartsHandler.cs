using CartingService.Domain.Interfaces.V1;
using CartingService.Domain.Models.V1;
using MediatR;

namespace CartingService.Domain.Queries.V1.GetCarts;

public class GetCartsHandler : IRequestHandler<GetCartsQuery, IEnumerable<Cart>>
{
    private readonly ICartsRepository _cartsRepository;

    public GetCartsHandler(ICartsRepository cartsRepository)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
    }

    public async Task<IEnumerable<Cart>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
    {
        return await _cartsRepository.GetAsync();
    }
}