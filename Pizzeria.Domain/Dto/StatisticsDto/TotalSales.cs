namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public record TotalSales
    {
        public DateTime SalesDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
