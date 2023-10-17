using System.ComponentModel.DataAnnotations;
using CartingService.Domain.Commands.V1.CreateCart;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V1;

public partial class CartingController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([Required] int id, [Required] string name, [Required] decimal price, [Required] int quantity, string image)
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
            });

            var location = $"v1/carting/{result.Id}";
            return Created(location, new CreatedResult(location, result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}