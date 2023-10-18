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

    public Task<Cart> GetAsync(int cartId, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var entities = db.GetCollection<CartEntity>(CollectionName).FindAll().ToList();
        var existingItem = entities.FirstOrDefault(x => x.CartId == cartId);

        return Task.FromResult(_mapper.Map<Cart>(existingItem));
    }

    public Task CreateAsync(Cart cart, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
         
        var entity = _mapper.Map<CartEntity>(cart);

       var entities = db.GetCollection<CartEntity>(CollectionName);
       entities.Insert(entity);
       
       return Task.CompletedTask;
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var entities = db.GetCollection<CartEntity>(CollectionName);

        entities.DeleteMany(t => t.CartId == id);

        return Task.CompletedTask;
    }
}