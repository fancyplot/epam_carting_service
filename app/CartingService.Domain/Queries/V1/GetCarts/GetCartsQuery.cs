using CartingService.Domain.Models.V1;
using MediatR;

namespace CartingService.Domain.Queries.V1.GetCarts;

public class GetCartsQuery : IRequest<Cart>
{
    public string CartId { get; set; }
}