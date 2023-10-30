namespace CartingService.Domain.Models.V2;

public class CartItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string CartId { get; set; }
}