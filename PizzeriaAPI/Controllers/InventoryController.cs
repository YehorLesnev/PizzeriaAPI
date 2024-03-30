using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.InventoryDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.InventoryService;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController(IInventoryService inventoryService) 
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<InventoryDto> GetAll()
        {
            return Mappers.MapInventoryToDto(inventoryService.GetAll(asNoTracking: true));
        }

        [HttpGet("{ingredientId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryDto>> Get(Guid ingredientId)
        {
            var inventory = await inventoryService.GetAsync(a => a.IngredientId.Equals(ingredientId), true);

            if(inventory is null) return NotFound();

            return Ok(Mappers.MapInventoryToDto(inventory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InventoryDto>> Create([FromBody] InventoryDto requestInventoryDto)
        {
            var inventory = Mappers.MapDtoToInventory(requestInventoryDto);
            
            await inventoryService.CreateAsync(inventory);

            return Ok(Mappers.MapInventoryToDto(inventory));
        }

        [HttpPut("{ingredientId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InventoryDto>> Update([FromRoute] Guid ingredientId, [FromBody] InventoryDto requestInventoryDto)
        {
            var initialInventory = await inventoryService.GetAsync(o => o.IngredientId.Equals(ingredientId), true);

            if(initialInventory is null) return NotFound();

            var updatedInventory = Mappers.MapDtoToInventory(requestInventoryDto);
            updatedInventory.IngredientId = initialInventory.IngredientId;

            await inventoryService.UpdateAsync(updatedInventory);
            
            return Ok(Mappers.MapInventoryToDto(updatedInventory));
        }

        [HttpDelete("{ingredientId:guid}")]
        public async Task Delete([FromRoute] Guid ingredientId)
        {
            var inventory = await inventoryService.GetAsync(o => o.IngredientId.Equals(ingredientId));

            if(inventory is null) return;

            await inventoryService.DeleteAsync(inventory);
        }
    }
}
