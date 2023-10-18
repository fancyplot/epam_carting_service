using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public partial class CartingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
