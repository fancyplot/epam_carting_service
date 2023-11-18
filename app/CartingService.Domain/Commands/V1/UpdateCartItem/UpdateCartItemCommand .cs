using MediatR;

namespace CartingService.Domain.Commands.V1.UpdateCartItem;

public class UpdateCartItemCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public decimal? Price { get; set; }
}