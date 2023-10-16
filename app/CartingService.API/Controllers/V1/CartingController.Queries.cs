using CartingService.Domain.Queries.V1.GetCarts;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V1;

public partial class CartingController
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _mediator.Send(new GetCartsQuery());

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }
}