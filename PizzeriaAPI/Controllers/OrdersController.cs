﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Domain.Dto.IngredientDto;
using Pizzeria.Domain.Dto.OrderDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Services.IngredientService;
using Pizzeria.Domain.Services.OrderService;
using PizzeriaAPI.Identity.Roles;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService)
        : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}")]
        public IEnumerable<ResponseOrderDto> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null
        )
        {
            var orders = orderService.GetAll(pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true)
                .ToList();

            return Mappers.MapOrderToResponseDto(orderService.GetAll(
                pageNumber: pageNumber,
                pageSize: pageSize,
                asNoTracking: true));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}, {UserRoleNames.Cashier}")]
        public async Task<ActionResult<ResponseOrderDto>> Get(Guid id)
        {
            var order = await orderService.GetAsync(a => a.OrderId.Equals(id), true);

            if(order is null) return NotFound();

            return Ok(Mappers.MapOrderToResponseDto(order));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseOrderDto>> Create([FromBody] RequestOrderDto requestOrderDto)
        {
            var order = Mappers.MapRequestDtoToOrder(requestOrderDto);
            
            await orderService.CreateAsync(order);

            return Ok(Mappers.MapOrderToResponseDto(order));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task<ActionResult<ResponseOrderDto>> Update([FromRoute] Guid id, [FromBody] RequestOrderDto requestOrderDto)
        {
            var initialOrder = await orderService.GetAsync(o => o.OrderId.Equals(id), true);

            if(initialOrder is null) return NotFound();

            var updatedOrder = Mappers.MapRequestDtoToOrder(requestOrderDto);
            updatedOrder.OrderId = initialOrder.OrderId;

            await orderService.UpdateAsync(updatedOrder);
            
            return Ok(Mappers.MapOrderToResponseDto(updatedOrder));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRoleNames.Admin}, {UserRoleNames.Manager}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var order = await orderService.GetAsync(o => o.OrderId.Equals(id));

            if(order is null) return;

            await orderService.DeleteAsync(order);
        }
    }
}
