namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public record AverageOrderTotalValue
    {
        public DateOnly Date { get; set; }
        public decimal AverageOrderTotal { get; set; }
    }
}
