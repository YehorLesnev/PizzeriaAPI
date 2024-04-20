namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public record AverageOrderTotalValueDay
    {
        public DateOnly Date { get; set; }
        public decimal AverageOrderTotal { get; set; }
    }
}
