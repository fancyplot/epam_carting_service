using CartingService.Domain.Models.V1;
using MediatR;

namespace CartingService.Domain.Commands.V1.CreateCart;

public class CreateCartCommand : IRequest<CartItem>
{
    public string CartId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}