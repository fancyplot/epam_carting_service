using CartingService.Domain.Models.V1;

namespace CartingService.Domain.Interfaces.V1;

public interface ICartsRepository
{
    Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken);

    Task<Cart> GetAsync(int cartId, CancellationToken cancellationToken);

    Task CreateAsync(Cart cart, CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);
}