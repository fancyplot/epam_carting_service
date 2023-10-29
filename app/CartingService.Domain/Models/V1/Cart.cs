namespace CartingService.Domain.Models.V1;

public class Cart
{
    public string Id { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
}