using CartingService.Domain.Models.V1;

namespace CartingService.Infrastructure.Models.V1;

public class CartEntity
{
    public string Id { get; set; }
    public List<CartItemEntity> CartItems { get; set; }
}