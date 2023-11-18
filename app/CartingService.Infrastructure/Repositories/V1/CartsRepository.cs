using AutoMapper;
using CartingService.Domain.Interfaces.V1;
using CartingService.Domain.Models.V1;
using CartingService.Infrastructure.Models.V1;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace CartingService.Infrastructure.Repositories.V1;

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

    public Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var entities = db.GetCollection<CartEntity>(CollectionName).FindAll().ToList();
        
        return Task.FromResult(_mapper.Map<IEnumerable<Cart>>(entities));
    }

    public Task<Cart> GetCartAsync(string cartId, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var existingCart = db.GetCollection<CartEntity>(CollectionName)
            .Include(t => t.CartItems)
            .FindOne(x => x.Id == cartId);

        return Task.FromResult(_mapper.Map<Cart>(existingCart));
    }

    public Task<CartItem> GetCartItemAsync(string cartId, int cartItemId, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var existingCart = db.GetCollection<CartEntity>(CollectionName)
            .Include(t => t.CartItems)
            .FindOne(x => x.Id == cartId);

        var existingItem = existingCart?.CartItems?.FirstOrDefault(p => p.Id == cartItemId);

        return Task.FromResult(_mapper.Map<CartItem>(existingItem));
    }

    public Task<IEnumerable<Cart>> GetCartItemsAsync(int cartItemId, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var existingCarts = db.GetCollection<CartEntity>(CollectionName)
            .Include(t => t.CartItems)
            .FindAll();

        var items = existingCarts.Where(t => t.CartItems.Any(p => p.Id == cartItemId));

        return Task.FromResult(_mapper.Map<IEnumerable<Cart>>(items));
    }

    public Task CreateCartAsync(string cartId, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var entity = new CartEntity()
        {
            Id = cartId
        };

        var entities = db.GetCollection<CartEntity>(CollectionName);
        entities.Insert(entity);
       
        return Task.CompletedTask;
    }

    public Task CreateCartItemAsync(CartItem cartItem, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);

        var existingCart = db.GetCollection<CartEntity>(CollectionName)
            .Include(t => t.CartItems)
            .FindOne(x => x.Id == cartItem.CartId);
        
        var cartItemEntity = _mapper.Map<CartItemEntity>(cartItem);

        if (existingCart.CartItems == null)
            existingCart.CartItems = new List<CartItemEntity>();

        existingCart.CartItems.Add(cartItemEntity);

        db.GetCollection<CartEntity>(CollectionName).Update(existingCart);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(string cartId, int cartItemId, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);

        var existingCart = db.GetCollection<CartEntity>(CollectionName)
            .Include(t => t.CartItems)
            .FindOne(x => x.Id == cartId);

        existingCart.CartItems.RemoveAll(p => p.Id == cartItemId);

        db.GetCollection<CartEntity>(CollectionName).Update(existingCart);

        return Task.CompletedTask;
    }

    public Task UpdateCartItemAsync(string cartId, int id, string? image, string? name, decimal? price, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);

        var existingCart = db.GetCollection<CartEntity>(CollectionName)
            .Include(t => t.CartItems)
            .FindOne(x => x.Id == cartId);

        var existingItem = existingCart.CartItems
            .FirstOrDefault(t => t.CartId == cartId && t.Id == id);

        if (image != null)
            existingItem.Image = image;

        if(name != null)
            existingItem.Name = name;

        if (price != null)
            existingItem.Price = (decimal)price;

        db.GetCollection<CartEntity>(CollectionName).Update(existingCart);

        return Task.CompletedTask;
    }
}