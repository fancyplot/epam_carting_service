using System.ComponentModel.DataAnnotations;
using CartingService.Domain.Commands.V1.CreateCart;
using CartingService.Domain.Commands.V1.DeleteCart;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V1;

public partial class CartingController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([Required] int id, [Required] string name, [Required] decimal price, [Required] int quantity,
        string image, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _mediator.Send(new CreateCartCommand()
            {
                Id = id,
                Name = name,
                Image = image,
                Price = price,
                Quantity = quantity
            }, cancellationToken);

            var location = $"v1/carting/{result.Id}";
            return Created(location, new CreatedResult(location, result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _mediator.Send(new DeleteCartCommand()
            {
                Id = id
            }, cancellationToken);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}