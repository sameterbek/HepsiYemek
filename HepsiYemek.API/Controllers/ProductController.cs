using HepsiYemek.Business.Handlers.Product.Command;
using HepsiYemek.Business.Handlers.Product.Query;
using HepsiYemek.Dto.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HepsiYemek.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            return Ok(await _mediator.Send(new GetProductQuery(id)));
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> ListAsync([FromQuery] GetProductFilter filter)
        {
            return Ok(await _mediator.Send(new GetProductsQuery(filter)));
        }

        [HttpPost()]
        public async Task<ActionResult> CreateAsync([FromBody] CreateProductDto command)
        {
            return Ok(await _mediator.Send(new CreateProductCommand(command)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(string id, [FromBody] UpdateProductDto command)
        {
            return Ok(await _mediator.Send(new UpdateProductCommand(id, command)));
        }


    }
}
