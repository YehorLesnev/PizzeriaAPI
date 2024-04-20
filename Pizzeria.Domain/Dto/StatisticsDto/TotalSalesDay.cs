namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public record TotalSalesDay
    {
        public DateTime SalesDate { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
