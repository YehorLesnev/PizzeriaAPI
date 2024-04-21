namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public class ProductOfMonth
    {
        public string Category { get; set; }
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
    }
}
