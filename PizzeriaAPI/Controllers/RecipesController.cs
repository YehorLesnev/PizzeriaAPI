using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.RecipeService;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipesController(IRecipeService recipeService) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseRecipeDto> GetAll()
        {
            return Mappers.MapRecipeToResponseDto(recipeService.GetAll(asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseRecipeDto>> Get(Guid id)
        {
            var recipe = await recipeService.GetAsync(a => a.RecipeId.Equals(id), true);

            if(recipe is null) return NotFound();

            return Ok(Mappers.MapRecipeToResponseDto(recipe));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseRecipeDto>> Create([FromBody] RequestRecipeDto requestRecipeDto)
        {
            var recipe = Mappers.MapRequestDtoToRecipe(requestRecipeDto);
            
            await recipeService.CreateAsync(recipe);

            return Ok(Mappers.MapRecipeToResponseDto(recipe));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseRecipeDto>> Update([FromRoute] Guid id, [FromBody] RequestRecipeDto requestRecipeDto)
        {
            var initialRecipe = await recipeService.GetAsync(o => o.RecipeId.Equals(id), true);

            if(initialRecipe is null) return NotFound();

            var updatedRecipe = Mappers.MapRequestDtoToRecipe(requestRecipeDto);
            updatedRecipe.RecipeId = initialRecipe.RecipeId;

            await recipeService.UpdateAsync(updatedRecipe);
            
            return Ok(Mappers.MapRecipeToResponseDto(updatedRecipe));
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var recipe = await recipeService.GetAsync(o => o.RecipeId.Equals(id));

            if(recipe is null) return;

            await recipeService.DeleteAsync(recipe);
        }
    }
}
