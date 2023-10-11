using Application.Features.Commands.CreateOrderCommand;
using Application.Features.Commands.UpdateOrderCommand;
using Application.Features.Queries.GetAllOrders;
using Application.Features.Queries.GetOrderByCodigo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Insert")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post([FromBody] CreateOrderCommand order)
        {
            var response = await _mediator.Send(order);

            return CreatedAtAction(nameof(Post), new { codigo = response });
        }

        [HttpPost("Update")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] UpdateOrderCommand order)
        {
            var response = await _mediator.Send(order);

            return CreatedAtAction(nameof(Post), new { codigo = response });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<OrderDTO>>> Get() 
        { 
            var orders = await _mediator.Send(new GetOrderQuery());

            return Ok(orders);
        }

        [HttpGet("{Codigo}")]
        public async Task<IActionResult> Get(int Codigo)
        {
            var ordem = await _mediator.Send(new GetOrderByCodigoQueueQuery(Codigo));

            return Ok(ordem);
        }

    }
}
