using HepsiYemek.Business.Handlers.Category.Command;
using HepsiYemek.Business.Handlers.Category.Query;
using HepsiYemek.Dto.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HepsiYemek.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            return Ok(await _mediator.Send(new GetCategoryQuery(id)));
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> ListAsync([FromQuery] GetCategoryFilter filter)
        {
            return Ok(await _mediator.Send(new GetCategoriesQuery(filter)));
        }

        [HttpPost()]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCategoryDto command)
        {
            return Ok(await _mediator.Send(new CreateCategoryCommand(command)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(string id, [FromBody] UpdateCategoryDto command)
        {
            return Ok(await _mediator.Send(new UpdateCategoryCommand(id, command)));
        }


    }
}
