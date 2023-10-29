using AutoMapper;
using CartingService.Domain.Interfaces.V1;
using CartingService.Domain.Models.V1;
using MediatR;

namespace CartingService.Domain.Commands.V1.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartItem>
{
    private readonly ICartsRepository _cartsRepository;
    private readonly IMapper _mapper;

    public CreateCartHandler(ICartsRepository cartsRepository, IMapper mapper)
    {
        _cartsRepository = cartsRepository ?? throw new ArgumentNullException(nameof(cartsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<CartItem> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var existingCart = await _cartsRepository.GetCartAsync(request.CartId, cancellationToken);
        if (existingCart == null)
            await _cartsRepository.CreateCartAsync(request.CartId, cancellationToken);

        existingCart = await _cartsRepository.GetCartAsync(request.CartId, cancellationToken);
        if (existingCart == null)
            throw new Exception($"Cart with id {request.CartId} was not created");

        var existingCartItem = await _cartsRepository.GetCartItemAsync(request.CartId, request.Id, cancellationToken);
        if (existingCartItem != null)
            throw new ArgumentException($"Cart item with id {request.Id} already exists in cart {request.CartId}");

        var cartItem = _mapper.Map<CartItem>(request);

        await _cartsRepository.CreateCartItemAsync(cartItem, cancellationToken);

        var createdCartItem = await _cartsRepository.GetCartItemAsync(request.CartId, request.Id, cancellationToken);

        if (createdCartItem == null)
            throw new Exception($"Cart item with id {request.Id} was not created");

        return createdCartItem;
    }
}