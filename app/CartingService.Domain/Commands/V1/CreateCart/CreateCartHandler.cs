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
        var existingCart = await _cartsRepository.GetAsync(request.Id, cancellationToken);
        if (existingCart != null)
            throw new ArgumentException($"Cart with id {request.Id} already exists");

        await _cartsRepository.CreateAsync(cart, cancellationToken);

        var createdCart = await _cartsRepository.GetAsync(request.Id, cancellationToken);

        if (createdCart == null)
            throw new Exception($"Cart with id {request.Id} was not created");

        return createdCart;
    }
}