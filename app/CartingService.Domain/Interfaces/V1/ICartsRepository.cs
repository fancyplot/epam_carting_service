using CartingService.Domain.Models.V1;

namespace CartingService.Domain.Interfaces.V1;

public interface ICartsRepository
{
    Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken);

    Task<Cart> GetCartAsync(string cartId, CancellationToken cancellationToken);

    Task<CartItem> GetCartItemAsync(string cartId, int cartItemId, CancellationToken cancellationToken);

    Task CreateCartAsync(string cartId, CancellationToken cancellationToken);

    Task CreateCartItemAsync(CartItem cartItem, CancellationToken cancellationToken);

    Task DeleteAsync(string cartId, int cartItemId, CancellationToken cancellationToken);

    Task<IEnumerable<Cart>> GetCartItemsAsync(int cartItemId, CancellationToken cancellationToken);

    Task UpdateCartItemAsync(string cartId, int id, string? image, string? name, decimal? price, CancellationToken cancellationToken);
}