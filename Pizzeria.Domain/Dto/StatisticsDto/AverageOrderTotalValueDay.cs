namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public class AverageOrderTotalValueDay
    {
        public DateOnly Date { get; set; }
        public decimal AverageOrderTotal { get; set; }
    }
}
