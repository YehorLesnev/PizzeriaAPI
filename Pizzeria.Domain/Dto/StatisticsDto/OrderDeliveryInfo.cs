namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public class OrderDeliveryInfo
    {
        public DateOnly Date { get; set; }
        public int NumberOfOrders { get; set; }
        public int NumberOfDelivery { get; set; }
        public short DeliveryPercentage =>
            (short)(NumberOfOrders == 0 ? 0 : (short)((float)NumberOfDelivery / (float)NumberOfOrders * 100.0));
    }
}
