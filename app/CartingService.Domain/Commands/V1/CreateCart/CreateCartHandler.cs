using AutoMapper;
using CartingService.Domain.Interfaces.V1;
using CartingService.Domain.Models.V1;
using MediatR;

namespace CartingService.Domain.Commands.V1.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, Cart>
{
    private readonly ICartsRepository _cartsRepository;
    private readonly IMapper _mapper;

    public CreateCartHandler(ICartsRepository cartsRepository, IMapper mapper)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = _mapper.Map<Cart>(request);
        return await _cartsRepository.CreateAsync(cart);
    }
}