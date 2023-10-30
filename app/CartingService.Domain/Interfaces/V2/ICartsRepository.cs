using CartingService.Domain.Models.V2;

namespace CartingService.Domain.Interfaces.V2;

public interface ICartsRepository
{
    Task<IEnumerable<CartItem>> GetCartItemsAsync(string cartId, CancellationToken cancellationToken);
}