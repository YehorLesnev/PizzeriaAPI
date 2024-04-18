using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.ItemDto;
using Pizzeria.Domain.Dto.RecipeDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.ItemService;
using Pizzeria.Domain.Services.RecipeService;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController(IItemService itemService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ResponseItemDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null,
            [FromQuery] string? itemCategory = null
            )
        {
            if (itemCategory is null)
                return Mappers.MapItemToResponseDto(itemService.GetAll(
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    asNoTracking: true));

            return Mappers.MapItemToResponseDto(itemService.GetAll(
                x => x.ItemCategory.Equals(itemCategory),
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseItemDto>> Get(Guid id)
        {
            var item = await itemService.GetAsync(a => a.ItemId.Equals(id), true);

            if (item is null) return NotFound();

            return Ok(Mappers.MapItemToResponseDto(item));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        [RequestFormLimits(MultipartBodyLengthLimit = 2_097_152)]
        public async Task<ActionResult<ResponseItemDto>> Create([FromForm] RequestItemDto requestItemDto)
        {
            var item = Mappers.MapRequestDtoToItem(requestItemDto);
            item.ItemId = Guid.NewGuid();

            item.ImagePath = await itemService.SaveItemImageAsync(
                requestItemDto.Image,
                requestItemDto.ItemCategory,
                requestItemDto.ItemName);

            await itemService.CreateAsync(item);

            var createdItem = await itemService.GetAsync(r => r.ItemId == item.ItemId);

            if(createdItem is null)
                return BadRequest("Couldn't create item");

            return Created(nameof(Get), Mappers.MapItemToResponseDto(createdItem));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task<ActionResult<ResponseItemDto>> Update([FromRoute] Guid id, [FromForm] RequestItemDto requestItemDto)
        {
            var initialItem = await itemService.GetAsync(o => o.ItemId.Equals(id), true);

            if (initialItem is null) return NotFound();

            var updatedItem = Mappers.MapRequestDtoToItem(requestItemDto);
            updatedItem.ItemId = initialItem.ItemId;

            updatedItem.ImagePath = await itemService.SaveItemImageAsync(
                requestItemDto.Image,
                requestItemDto.ItemCategory,
                requestItemDto.ItemName);

            await itemService.UpdateAsync(updatedItem);

            return Ok(Mappers.MapItemToResponseDto(updatedItem));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var item = await itemService.GetAsync(o => o.ItemId.Equals(id));

            if (item is null) return;

            await itemService.DeleteAsync(item);
        }
    }
}
