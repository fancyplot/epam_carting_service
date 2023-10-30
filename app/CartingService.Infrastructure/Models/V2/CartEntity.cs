
namespace CartingService.Infrastructure.Models.V2;

public class CartEntity
{
    public string Id { get; set; }
    public List<CartItemEntity> CartItems { get; set; }
}