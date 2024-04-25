using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Repository.StaffRepository;
using Pizzeria.Domain.Repository.StatisticsRepository;
using Pizzeria.Domain.Services.OrderService;
using System;

namespace Pizzeria.Domain.Services.Statistics
{
    public class StatisticsService(
        IStatisticsRepository statisticsRepository,
        IOrderService orderService,
        IStaffRepository staffRepository) : IStatisticsService
    {
        public IEnumerable<StaffPayrollResult> CalculateStaffPayroll(DateTime startDate, DateTime endDate) =>
            statisticsRepository.CalculateStaffPayroll(startDate, endDate).OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

        public IEnumerable<TotalSales> GetTotalSalesRevenueByDay(
            DateTime startDate,
            DateTime endDate,
            string? itemCategory = null) =>
            itemCategory is null ? statisticsRepository.GetTotalSalesRevenueByDay(startDate, endDate)
            : statisticsRepository.GetSalesDistributionByCategoryAndDay(itemCategory, startDate, endDate);


        public IEnumerable<TotalSales> GetTotalSalesRevenueByMonth(DateTime startDate, DateTime endDate, string? itemCategory = null)
        {
            var start = new DateTime(startDate.Year, startDate.Month, 1);
            var end = new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

            var salesDays = itemCategory is null ?
                statisticsRepository.GetTotalSalesRevenueByDay(start, end)
                : statisticsRepository.GetSalesDistributionByCategoryAndDay(itemCategory, start, end);

            return salesDays
                .GroupBy(s => new { s.SalesDate.Year, s.SalesDate.Month })
                .Select(g => new TotalSales
                {
                    SalesDate = new DateTime(g.Key.Year, g.Key.Month, DateTime.DaysInMonth(g.Key.Year, g.Key.Month)),
                    TotalRevenue = g.Sum(s => s.TotalRevenue)
                });
        }

        public IEnumerable<TotalSales> GetTotalSalesRevenueByYear(DateTime startDate, DateTime endDate, string? itemCategory = null)
        {
            var start = new DateTime(startDate.Year, 1, 1);
            var end = new DateTime(endDate.Year, 12, DateTime.DaysInMonth(endDate.Year, 12));

            var salesDays = itemCategory is null ?
                statisticsRepository.GetTotalSalesRevenueByDay(start, end)
                : statisticsRepository.GetSalesDistributionByCategoryAndDay(itemCategory, start, end);

            return salesDays
                .GroupBy(s => s.SalesDate.Year)
                .Select(g => new TotalSales
                {
                    SalesDate = new DateTime(g.Key, 12, DateTime.DaysInMonth(g.Key, 12)),
                    TotalRevenue = g.Sum(s => s.TotalRevenue)
                });
        }

        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByDay(DateTime startDate, DateTime endDate) =>
            statisticsRepository.GetAverageOrderValueByDay(startDate, endDate);


        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByMonth(DateTime startDate, DateTime endDate) =>
            statisticsRepository.GetAverageOrderValueByMonth(
                new DateTime(startDate.Year, startDate.Month, 1),
                new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month)));


        public IEnumerable<AverageOrderTotalValue> GetAverageOrderValueByYear(DateTime startDate, DateTime endDate) =>
            statisticsRepository.GetAverageOrderValueByYear(
                new DateTime(startDate.Year, 1, 1),
                new DateTime(endDate.Year, 12, DateTime.DaysInMonth(endDate.Year, 12)));

        public ProductOfMonth? GetProductOfMonth(int year, int month) =>
            statisticsRepository.GetProductOfMonth(year, month);

        public IEnumerable<OrderDeliveryInfo> GetOrderDeliveryInfoByDay(DateTime startDate, DateTime endDate)
        {
            var orders = orderService
                .GetAll(x => x.Date >= startDate && x.Date <= endDate, asNoTracking: true)
                .ToList();

            return orders
                .GroupBy(x => new DateOnly(x.Date.Year, x.Date.Month, x.Date.Day))
                .Select(g => new OrderDeliveryInfo
                {
                    Date = g.Key,
                    NumberOfOrders = orders.Count(x => new DateOnly(x.Date.Year, x.Date.Month, x.Date.Day) == g.Key),
                    NumberOfDelivery = orders
                        .Count(x => new DateOnly(x.Date.Year, x.Date.Month, x.Date.Day) == g.Key && x.Delivery)
                });
        }

        public IEnumerable<OrderDeliveryInfo> GetOrderDeliveryInfoByMonth(DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            endDate = new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

            var orders = orderService
                .GetAll(x => x.Date >= startDate && x.Date <= endDate, asNoTracking: true)
                .ToList();

            return orders
                .GroupBy(x => new DateOnly(x.Date.Year, x.Date.Month, 1))
                .Select(g => new OrderDeliveryInfo
                {
                    Date = g.Key,
                    NumberOfOrders = orders.Count(x => new DateOnly(x.Date.Year, x.Date.Month, 1) == g.Key),
                    NumberOfDelivery = orders
                        .Count(x => x.Delivery && new DateOnly(x.Date.Year, x.Date.Month, 1) == g.Key)
                });
        }

        public IEnumerable<OrderDeliveryInfo> GetOrderDeliveryInfoByYear(DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, 1, 1);
            endDate = new DateTime(endDate.Year, 12, DateTime.DaysInMonth(endDate.Year, 12));

            var orders = orderService
                .GetAll(x => x.Date >= startDate && x.Date <= endDate, asNoTracking: true)
                .ToList();

            return orders
                .GroupBy(x => x.Date.Year)
                .Select(g => new OrderDeliveryInfo
                {
                    Date = new DateOnly(g.Key, 1, 1),
                    NumberOfOrders = orders.Count(x => x.Date.Year == g.Key),
                    NumberOfDelivery = orders
                        .Count(x => x.Delivery && x.Date.Year == g.Key)
                });
        }

        public IEnumerable<StaffOrdersInfo> GetStaffOrdersInfoByDay(DateOnly date)
        {
            var staff = staffRepository.GetAllWithOrders(asNoTracking: true).ToList();

            return staff.Select(x => new StaffOrdersInfo
            {
                Staff = Mappers.MapStaffToResponseDto(x),
                Date = date,
                NumberOfOrders = x.Orders.Count(o => DateOnly.FromDateTime(o.Date) == date),
                OrdersTotalSum = x.Orders.Where(o => DateOnly.FromDateTime(o.Date) == date)
                    .Select(o => o.Total)
                    .DefaultIfEmpty()
                    .Sum(),
                AverageOrderTotal = x.Orders.Where(o => DateOnly.FromDateTime(o.Date) == date)
                    .Select(o => o.Total)
                    .DefaultIfEmpty()
                    .Average()
            })
                .OrderByDescending(x => x.NumberOfOrders);
        }

        public IEnumerable<StaffOrdersInfo> GetStaffOrdersInfoByMonth(DateOnly date)
        {
            var staff = staffRepository.GetAllWithOrders(asNoTracking: true);

            return staff.Select(x => new StaffOrdersInfo
            {
                Staff = Mappers.MapStaffToResponseDto(x),
                Date = date,
                NumberOfOrders = x.Orders.Count(o => o.Date.Month == date.Month && o.Date.Year == date.Year),
                OrdersTotalSum = x.Orders.Where(o => o.Date.Month == date.Month && o.Date.Year == date.Year)
                    .Select(o => o.Total)
                    .DefaultIfEmpty()
                    .Sum(),
                AverageOrderTotal = x.Orders.Where(o => o.Date.Month == date.Month && o.Date.Year == date.Year)
                    .Select(o => o.Total)
                    .DefaultIfEmpty()
                    .Average()
            })
            .OrderByDescending(x => x.NumberOfOrders);
        }

        public IEnumerable<StaffOrdersInfo> GetStaffOrdersInfoByYear(DateOnly date)
        {
            var staff = staffRepository.GetAllWithOrders(asNoTracking: true);

            return staff.Select(x => new StaffOrdersInfo
            {
                Staff = Mappers.MapStaffToResponseDto(x),
                Date = date,
                NumberOfOrders = x.Orders.Count(o => o.Date.Year == date.Year),
                OrdersTotalSum = x.Orders.Where(o => o.Date.Year == date.Year)
                    .Select(o => o.Total)
                    .DefaultIfEmpty()
                    .Sum(),
                AverageOrderTotal = x.Orders.Where(o => o.Date.Year == date.Year)
                    .Select(o => o.Total)
                    .DefaultIfEmpty()
                    .Average()
            })
            .OrderByDescending(x => x.NumberOfOrders);
        }

        public IEnumerable<NumberOfOrdersInfo> GetNumberOfOrdersByDay(DateOnly dateStart, DateOnly dateEnd)
        {
            var orders = orderService
                .GetAll(o => DateOnly.FromDateTime(o.Date) >= dateStart && DateOnly.FromDateTime(o.Date) <= dateEnd,
                    asNoTracking: true)
                .ToList();

            return orders.GroupBy(o => DateOnly.FromDateTime(o.Date))
                .Select(g => new NumberOfOrdersInfo
                {
                    Date = g.Key,
                    NumberOfOrders = orders.Count(o => DateOnly.FromDateTime(o.Date) == g.Key)
                });
        }

        public IEnumerable<NumberOfOrdersInfo> GetNumberOfOrdersByMonth(DateOnly dateStart, DateOnly dateEnd)
        {
            var start = new DateTime(dateStart.Year, dateStart.Month, 1);
            var end = new DateTime(dateEnd.Year, dateEnd.Month, DateTime.DaysInMonth(dateEnd.Year, dateEnd.Month));

            var orders = orderService
                .GetAll(o => o.Date >= start && o.Date <= end,
                    asNoTracking: true)
                .ToList();

            return orders.GroupBy(o => new DateOnly(o.Date.Year, o.Date.Month, 1))
                .Select(g => new NumberOfOrdersInfo
                {
                    Date = g.Key,
                    NumberOfOrders = orders.Count(o => new DateOnly(o.Date.Year, o.Date.Month, 1) == g.Key)
                });
        }

        public IEnumerable<NumberOfOrdersInfo> GetNumberOfOrdersByYear(DateOnly dateStart, DateOnly dateEnd)
        {
            var orders = orderService
                .GetAll(o =>
                    o.Date.Year >= dateStart.Year &&
                    o.Date.Year <= dateEnd.Year,
                    asNoTracking: true)
                .ToList();

            return orders.GroupBy(o => o.Date.Year)
                .Select(g => new NumberOfOrdersInfo
                {
                    Date = new DateOnly(g.Key, 1, 1),
                    NumberOfOrders = orders.Count(o => o.Date.Year == g.Key)
                });
        }
    }
}