using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.ItemDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.ItemService;

namespace PizzeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IItemService itemService)
        : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ResponseItemDto> GetAll()
        {
            return Mappers.MapItemToResponseDto(itemService.GetAll(asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseItemDto>> Get(Guid id)
        {
            var item = await itemService.GetAsync(a => a.ItemId.Equals(id), true);

            if(item is null) return NotFound();

            return Ok(Mappers.MapItemToResponseDto(item));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseItemDto>> Create([FromBody] RequestItemDto requestItemDto)
        {
            var item = Mappers.MapRequestDtoToItem(requestItemDto);
            
            await itemService.CreateAsync(item);

            return Ok(Mappers.MapItemToResponseDto(item));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseItemDto>> Update([FromRoute] Guid id, [FromBody] RequestItemDto requestItemDto)
        {
            var initialItem = await itemService.GetAsync(o => o.ItemId.Equals(id), true);

            if(initialItem is null) return NotFound();

            var updatedItem = Mappers.MapRequestDtoToItem(requestItemDto);
            updatedItem.ItemId = initialItem.ItemId;

            await itemService.UpdateAsync(updatedItem);
            
            return Ok(Mappers.MapItemToResponseDto(updatedItem));
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var item = await itemService.GetAsync(o => o.ItemId.Equals(id));

            if(item is null) return;

            await itemService.DeleteAsync(item);
        }
    }
}
