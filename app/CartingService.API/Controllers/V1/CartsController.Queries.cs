using CartingService.Domain.Models.V1;
using CartingService.Domain.Queries.V1.GetCarts;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V1;

public partial class CartsController
{
    [HttpGet("{cartId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cart))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(string cartId, CancellationToken cancellationToken = default)
    {
        if (String.IsNullOrEmpty(cartId))
            return BadRequest("Cart Id should not be null or empty.");

        var result = await _mediator.Send(new GetCartsQuery()
        {
            CartId = cartId
        }, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}