using AutoMapper;
using CartingService.Domain.Interfaces.V1;
using CartingService.Domain.Models.V1;
using CartingService.Infrastructure.Dto.V1;
using CartingService.Infrastructure.Models.V1;
using LiteDB;

namespace CartingService.Infrastructure.Repositories.V1;

public class CartsRepository : ICartsRepository
{
    private readonly IMapper _mapper;

    public CartsRepository(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<Cart>> GetAsync()
    {
        using var db = new LiteDatabase(@"D:\Temp\MyData.db");
        var entitiesDto = db.GetCollection<CartEntityDto>("carts").FindAll().ToList();
        var entities = _mapper.Map<IEnumerable<CartEntity>>(entitiesDto);
            
        return Task.FromResult(_mapper.Map<IEnumerable<Cart>>(entities));
    }
}