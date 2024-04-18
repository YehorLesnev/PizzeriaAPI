using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.RecipeService;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
    public class RecipesController(IRecipeService recipeService) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ResponseRecipeDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            return Mappers.MapRecipeToResponseDto(recipeService.GetAll(
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseRecipeDto>> GetAsync(Guid id)
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
            recipe.RecipeId = Guid.NewGuid();

            await recipeService.CreateAsync(recipe);

            var createdRecipe = await recipeService.GetAsync(r => r.RecipeId == recipe.RecipeId);

            if(createdRecipe is null)
                return BadRequest("Couldn't create recipe");

            return Created(nameof(GetAsync), Mappers.MapRecipeToResponseDto(createdRecipe));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseRecipeDto>> Update([FromRoute] Guid id, [FromBody] RequestRecipeDto requestRecipeDto)
        {
            var initialRecipe = await recipeService.GetAsync(o => o.RecipeId.Equals(id), false);

            if(initialRecipe is null) return NotFound("Couldn't find recipe with specified id");

            var updatedRecipe = Mappers.MapRequestDtoToRecipe(requestRecipeDto);

            initialRecipe.RecipeIngredients.Clear();
            initialRecipe.RecipeName = updatedRecipe.RecipeName;
            initialRecipe.CookingTime = updatedRecipe.CookingTime;

            foreach(var recipeIngredient in updatedRecipe.RecipeIngredients)
            {
                recipeIngredient.RecipeId = id;
                initialRecipe.RecipeIngredients.Add(recipeIngredient);
            }

            await recipeService.UpdateAsync(initialRecipe);

            return await GetAsync(id);
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
