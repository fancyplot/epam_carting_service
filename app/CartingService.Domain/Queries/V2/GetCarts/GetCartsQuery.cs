using CartingService.Domain.Models.V2;
using MediatR;

namespace CartingService.Domain.Queries.V2.GetCarts;

public class GetCartsQuery : IRequest<IEnumerable<CartItem>>
{
    public string CartId { get; set; }
}