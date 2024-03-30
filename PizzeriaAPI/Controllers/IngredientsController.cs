using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.IngredientDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.IngredientService;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController(IIngredientService ingredientService)
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseIngredientDto> GetAll()
        {
            return Mappers.MapIngredientToResponseDto(ingredientService.GetAll(asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseIngredientDto>> Get(Guid id)
        {
            var ingredient = await ingredientService.GetAsync(a => a.IngredientId.Equals(id), true);

            if(ingredient is null) return NotFound();

            return Ok(Mappers.MapIngredientToResponseDto(ingredient));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseIngredientDto>> Create([FromBody] RequestIngredientDto requestIngredientDto)
        {
            var ingredient = Mappers.MapRequestDtoToIngredient(requestIngredientDto);
            
            await ingredientService.CreateAsync(ingredient);

            return Ok(Mappers.MapIngredientToResponseDto(ingredient));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        public async Task Delete([FromRoute] Guid id)
        {
            var ingredient = await ingredientService.GetAsync(o => o.IngredientId.Equals(id));

            if(ingredient is null) return;

            await ingredientService.DeleteAsync(ingredient);
        }
    }
}
