using Asp.Versioning;
using CartingService.Domain.Models.V2;
using CartingService.Domain.Queries.V2.GetCarts;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V2;

public partial class CartsController
{
    [MapToApiVersion("2.0")]
    [HttpGet("{cartId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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