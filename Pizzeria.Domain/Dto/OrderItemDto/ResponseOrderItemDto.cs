using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Domain.Dto.OrderItemDto
{
    public class ResponseOrderItemDto
    {
        [Required] 
        public ResponseOrderItemInfoDto Item { get; set; }

        [Required]
        public required int Quantity { get; set; }
    }
}
