using MediatR;

namespace CartingService.Domain.Commands.V1.DeleteCart;

public class DeleteCartItemCommand : IRequest
{
    public int Id { get; set; }
    public string CartId { get; set; }
}