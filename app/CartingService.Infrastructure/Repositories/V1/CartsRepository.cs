using System.Data.Common;
using AutoMapper;
using CartingService.Domain.Interfaces.V1;
using CartingService.Domain.Models.V1;
using CartingService.Infrastructure.Dto.V1;
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

    public Task<IEnumerable<Cart>> GetAsync()
    {
        using var db = new LiteDatabase(_configuration["Database"]);
        var entitiesDto = db.GetCollection<CartEntityDto>(CollectionName).FindAll().ToList();
        var entities = _mapper.Map<IEnumerable<CartEntity>>(entitiesDto);
            
        return Task.FromResult(_mapper.Map<IEnumerable<Cart>>(entities));
    }

    public Task<Cart> CreateAsync(Cart cart)
    {
        using (var db = new LiteDatabase(_configuration["Database"]))
        {
            var entity = _mapper.Map<CartEntity>(cart);

            var entitiesDto = db.GetCollection<CartEntityDto>(CollectionName);
            var existingItem = entitiesDto.FindAll().FirstOrDefault(x => x.CartId == entity.Id);
            if (existingItem == null)
            {
                var mappedEntity = _mapper.Map<CartEntityDto>(entity);
                entitiesDto.Insert(mappedEntity);
            }
            else
                throw new ArgumentException($"Cart with id {existingItem.CartId} already exists");

            var createdItem = entitiesDto.FindAll().FirstOrDefault(x => x.CartId == entity.Id);
            var createdEntity = _mapper.Map<CartEntity>(createdItem);

            return Task.FromResult(_mapper.Map<Cart>(createdEntity));
        }

    }
}