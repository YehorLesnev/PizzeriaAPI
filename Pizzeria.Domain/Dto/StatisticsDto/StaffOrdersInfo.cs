using Pizzeria.Domain.Dto.StaffDto;

namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public class StaffOrdersInfo
    {
        public ResponseStaffDto Staff { get; set; }

        public DateOnly Date { get; set; }  

        public int NumberOfOrders { get; set; }

        public decimal OrdersTotalSum { get; set; }

        public decimal AverageOrderTotal { get; set; }
    }
}
