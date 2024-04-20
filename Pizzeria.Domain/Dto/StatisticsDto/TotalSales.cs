namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public record TotalSales
    {
        public DateTime SalesDate { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
