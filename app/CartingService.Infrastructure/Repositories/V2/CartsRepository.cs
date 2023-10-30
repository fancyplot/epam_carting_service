using AutoMapper;
using CartingService.Domain.Interfaces.V2;
using CartingService.Domain.Models.V2;
using CartingService.Infrastructure.Models.V2;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace CartingService.Infrastructure.Repositories.V2;

public class CartsRepository : ICartsRepository
{
    private const string CollectionName = "carts";
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CartsRepository(IMapper mapper, IConfiguration configuration)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public Task<IEnumerable<CartItem>> GetCartItemsAsync(string cartId, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var existingCart = db.GetCollection<CartEntity>(CollectionName)
            .Include(t => t.CartItems)
            .FindOne(x => x.Id == cartId);

        var items = existingCart?.CartItems;

        return Task.FromResult(_mapper.Map<IEnumerable<CartItem>>(items));
    }
}