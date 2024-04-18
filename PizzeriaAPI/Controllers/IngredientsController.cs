using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.CustomerDto;
using Pizzeria.Domain.Dto.IngredientDto;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.CustomerService;
using Pizzeria.Domain.Services.IngredientService;
using Pizzeria.Domain.Services.RecipeService;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IngredientsController(IIngredientService ingredientService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ResponseIngredientDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            return Mappers.MapIngredientToResponseDto(ingredientService.GetAll(
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseIngredientDto>> Get(Guid id)
        {
            var ingredient = await ingredientService.GetAsync(a => a.IngredientId.Equals(id), true);

            if(ingredient is null) return NotFound();

            return Ok(Mappers.MapIngredientToResponseDto(ingredient));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task<ActionResult<ResponseIngredientDto>> Create([FromBody] RequestIngredientDto requestIngredientDto)
        {
            var ingredient = Mappers.MapRequestDtoToIngredient(requestIngredientDto);
            ingredient.IngredientId = Guid.NewGuid();
            await ingredientService.CreateAsync(ingredient);

            var createdIngredient = await ingredientService.GetAsync(r => r.IngredientId == ingredient.IngredientId);

            if(createdIngredient is null)
                return BadRequest("Couldn't create ingredient");

            return Created(nameof(Get), Mappers.MapIngredientToResponseDto(createdIngredient));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task<ActionResult<ResponseIngredientDto>> Update([FromRoute] Guid id, [FromBody] RequestIngredientDto requestIngredientDto)
        {
            var initialIngredient = await ingredientService.GetAsync(o => o.IngredientId.Equals(id), true);

            if(initialIngredient is null) return NotFound();

            var updatedIngredient = Mappers.MapRequestDtoToIngredient(requestIngredientDto);
            updatedIngredient.IngredientId = initialIngredient.IngredientId;

            await ingredientService.UpdateAsync(updatedIngredient);
            
            return Ok(Mappers.MapIngredientToResponseDto(updatedIngredient));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var ingredient = await ingredientService.GetAsync(o => o.IngredientId.Equals(id));

            if(ingredient is null) return;

            await ingredientService.DeleteAsync(ingredient);
        }
    }
}
