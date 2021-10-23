using HepsiYemek.Business.Handlers.Category.Command;
using HepsiYemek.Dto.Category;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> CreateAsync(string id, [FromBody] CreateCategoryDto command)
        {
            return Ok(await _mediator.Send(new CreateCateogryCommand(id, command)));
        }
    }
}
