using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.UseCases.Category.Commands.CreateCategory;
using ECommerce.Application.UseCases.Category.Commands.DeleteCategory;
using ECommerce.Application.UseCases.Category.Commands.UpdateCategory;
using ECommerce.Application.UseCases.Category.Queries.GetAllCategories;
using ECommerce.Application.UseCases.Category.Queries.GetCategoryById;
using ECommerce.Application.UseCases.Category.Queries.GetCategoryByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ISender _mediator;

        public CategoriesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var category = await _mediator.Send(new GetCategoryByNameQuery(name));
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto command)
        {
            var category = await _mediator.Send(new CreateCategoryCommand(command));
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto command)
        {
            var category = await _mediator.Send(new UpdateCategoryCommand(id, command));
            if (category == false)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}
