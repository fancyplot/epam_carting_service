using System.ComponentModel.DataAnnotations;
using CartingService.Domain.Commands.V1.CreateCart;
using CartingService.Domain.Commands.V1.DeleteCart;
using CartingService.Domain.Models.V1;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V1;

public partial class CartsController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync([Required] string cartId, [Required] int itemId, [Required] string name, [Required] decimal price, [Required] int quantity,
        string? image = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(new CreateCartCommand()
            {
                CartId = cartId,
                Id = itemId,
                Name = name,
                Image = image,
                Price = price,
                Quantity = quantity
            }, cancellationToken);

            var location = $"v1/carts/{result.CartId}";
            return Created(location, new CreatedResult(location, result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([Required] string cartId, [Required] int itemId, CancellationToken cancellationToken = default)
    {
        try
        {
            await _mediator.Send(new DeleteCartItemCommand()
            {
                Id = itemId,
                CartId = cartId
            }, cancellationToken);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}