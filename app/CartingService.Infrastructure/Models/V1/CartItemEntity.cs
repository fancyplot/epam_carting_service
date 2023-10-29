namespace CartingService.Infrastructure.Models.V1;

public class CartItemEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string CartId { get; set; }
}