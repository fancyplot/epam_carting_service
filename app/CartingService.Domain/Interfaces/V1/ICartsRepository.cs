using CartingService.Domain.Models.V1;

namespace CartingService.Domain.Interfaces.V1;

public interface ICartsRepository
{
    Task<IEnumerable<Cart>> GetAsync();
}