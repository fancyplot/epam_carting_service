using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CartingService.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public partial class CartingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/<CartingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
