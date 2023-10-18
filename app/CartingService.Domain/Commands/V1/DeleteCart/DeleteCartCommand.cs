using MediatR;

namespace CartingService.Domain.Commands.V1.DeleteCart;

public class DeleteCartCommand : IRequest
{
    public int Id { get; set; }
}