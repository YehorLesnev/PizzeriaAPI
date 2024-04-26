using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Pizzeria.Domain.Dto.OrderDto;
using Pizzeria.Domain.Dto.StatisticsDto;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Mapper;
using Pizzeria.Domain.Models;
using Pizzeria.Domain.Services.LogService;
using Pizzeria.Domain.Services.OrderService;
using Pizzeria.Domain.Services.Statistics;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoleNames.Manager)]
    public class DownloadsController(
        IStatisticsService statisticsService,
        IOrderService orderService,
        ILogService logService) : ControllerBase
    {
        [HttpGet("PDF/StaffPayroll")]
        public ActionResult GetStaffPayrollPdf(
        [FromQuery] DateTime dateStart,
        [FromQuery] DateTime dateEnd)
        {
            var staffPayrollResults = statisticsService.CalculateStaffPayroll(dateStart, dateEnd).ToList();

            if (staffPayrollResults.IsNullOrEmpty())
                return BadRequest("Please specify correct start and end date");

            using MemoryStream stream = new MemoryStream();
            PdfDocument document = new PdfDocument();
            XFont headerFont = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12, XFontStyle.Regular);
            XGraphics gfx = null;

            int xPos = 50;
            int yPos = 50;
            int cellPadding = 10;
            int currentRow = 0;
            int maxRowsPerPage = 29;

            // Define table headers
            string[] headers = { "Name", "Position", "Hours Worked", "Hourly Rate ($)", "Payroll ($)" };
            int numColumns = headers.Length;
            int cellWidth = 0;

            foreach (var staffPayrollResult in staffPayrollResults)
            {
                if (currentRow == 0)
                {
                    var page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);

                    // Heading
                    gfx.DrawString($"Staff Payroll for {DateOnly.FromDateTime(dateStart)}-{DateOnly.FromDateTime(dateEnd)}",
                        headerFont, XBrushes.Black,
                        new XRect(xPos, yPos, page.Width - 2 * xPos, font.Height),
                        XStringFormats.Center);

                    yPos += headerFont.Height + cellPadding;

                    cellWidth = (int)((page.Width - 2 * xPos) / numColumns);

                    // Draw headers
                    for (int i = 0; i < numColumns; i++)
                    {
                        gfx.DrawString(headers[i], font, XBrushes.Black, new XRect(xPos + i * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                    }

                    yPos += font.Height + cellPadding;
                }

                // Draw row data
                gfx.DrawString($"{staffPayrollResult.FirstName} {staffPayrollResult.LastName}", font, XBrushes.Black, new XRect(xPos, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffPayrollResult.Position, font, XBrushes.Black, new XRect(xPos + cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffPayrollResult.HoursWorked.ToString(), font, XBrushes.Blue, new XRect(xPos + 2 * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffPayrollResult.HourlyRate.ToString(), font, XBrushes.Black, new XRect(xPos + 3 * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffPayrollResult.Payroll.ToString(), font, XBrushes.Green, new XRect(xPos + 4 * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                yPos += font.Height + cellPadding;
                currentRow++;

                // Check if the maximum rows per page is reached
                if (currentRow < maxRowsPerPage) continue;

                currentRow = 0;
                yPos = 50;
            }
            document.Save(stream, false);

            return File(stream.ToArray(), "application/pdf", "staff_payroll.pdf");
        }

        [HttpGet("XML/StaffPayroll")]
        public ActionResult GetStaffPayrollXml(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            var staffPayrollResults =
                statisticsService.CalculateStaffPayroll(dateStart, dateEnd)
                .ToList();

            // Create XML serializer for the StaffPayrollResult type
            var serializer = new XmlSerializer(typeof(List<StaffPayrollResult>));

            using MemoryStream stream = new MemoryStream();

            // Serialize the list of StaffPayrollResult objects to XML
            serializer.Serialize(stream, staffPayrollResults);

            // Reset the stream position to the beginning
            stream.Seek(0, SeekOrigin.Begin);

            // Set content type to XML and return the stream as a file
            return File(stream.ToArray(), "application/xml", "staff_payroll.xml");
        }

        [HttpGet("JSON/StaffPayroll")]
        public ActionResult GetStaffPayrollJson(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            var staffPayrollResults =
                statisticsService.CalculateStaffPayroll(dateStart, dateEnd)
                .ToList();

            // Serialize the list of StaffPayrollResult objects to JSON
            var json = JsonSerializer.Serialize(staffPayrollResults, new JsonSerializerOptions { WriteIndented = true });

            // Convert the JSON string to bytes
            var data = Encoding.UTF8.GetBytes(json);

            // Set content type to JSON and return the data as a file
            return File(data, "application/json", "staff_payroll.json");
        }

        [HttpGet("XML/StaffOrdersInfo")]
        public ActionResult GetStaffOrdersInfoXml(
            [FromQuery] DateOnly date)
        {
            var staffOrdersInfos =
                statisticsService.GetStaffOrdersInfoByMonth(date)
                    .ToList();

            // Create XML serializer for the StaffPayrollResult type
            var serializer = new XmlSerializer(typeof(List<StaffOrdersInfo>));

            using MemoryStream stream = new MemoryStream();

            // Serialize the list of StaffPayrollResult objects to XML
            serializer.Serialize(stream, staffOrdersInfos);

            // Reset the stream position to the beginning
            stream.Seek(0, SeekOrigin.Begin);

            // Set content type to XML and return the stream as a file
            return File(stream.ToArray(), "application/xml", "staff_orders_info.xml");
        }

        [HttpGet("PDF/StaffOrdersInfo")]
        public ActionResult GetStaffPayrollPdf(
        [FromQuery] DateOnly date)
        {
            var staffOrdersInfos =
                statisticsService.GetStaffOrdersInfoByMonth(date)
                .ToList();

            if (staffOrdersInfos.IsNullOrEmpty())
                return BadRequest("Please specify correct start and end date");

            using MemoryStream stream = new MemoryStream();
            PdfDocument document = new PdfDocument();
            XFont headerFont = new XFont("Arial", 12, XFontStyle.Bold);
            XFont font = new XFont("Arial", 12, XFontStyle.Regular);
            XGraphics gfx = null;

            int xPos = 50;
            int yPos = 50;
            int cellPadding = 10;
            int currentRow = 0;
            int maxRowsPerPage = 29;

            // Define table headers
            string[] headers = { "Name", "Position", "Num of orders", "Orders total sum", "Order total avg" };
            int numColumns = headers.Length;
            int cellWidth = 0;

            foreach (var staffOrdersInfo in staffOrdersInfos)
            {
                if (currentRow == 0)
                {
                    var page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);

                    // Heading
                    gfx.DrawString($"Staff Orders Info for {date}",
                        headerFont, XBrushes.Black,
                        new XRect(xPos, yPos, page.Width - 2 * xPos, font.Height),
                        XStringFormats.Center);

                    yPos += headerFont.Height + cellPadding;

                    cellWidth = (int)((page.Width - 2 * xPos) / numColumns);

                    // Draw headers
                    for (int i = 0; i < numColumns; i++)
                    {
                        gfx.DrawString(headers[i], font, XBrushes.Black, new XRect(xPos + i * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                    }

                    yPos += font.Height + cellPadding;
                }

                // Draw row data
                gfx.DrawString($"{staffOrdersInfo.Staff.FirstName} {staffOrdersInfo.Staff.LastName}", font, XBrushes.Black, new XRect(xPos, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffOrdersInfo.Staff.Position, font, XBrushes.Black, new XRect(xPos + cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffOrdersInfo.NumberOfOrders.ToString(), font, XBrushes.Blue, new XRect(xPos + 2 * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffOrdersInfo.OrdersTotalSum.ToString(CultureInfo.InvariantCulture) + " $", font, XBrushes.Black, new XRect(xPos + 3 * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                gfx.DrawString(staffOrdersInfo.AverageOrderTotal.ToString("0.00") + " $", font, XBrushes.Green, new XRect(xPos + 4 * cellWidth, yPos, cellWidth, font.Height), XStringFormats.Center);
                yPos += font.Height + cellPadding;
                currentRow++;

                // Check if the maximum rows per page is reached
                if (currentRow < maxRowsPerPage) continue;

                currentRow = 0;
                yPos = 50;
            }
            document.Save(stream, false);

            return File(stream.ToArray(), "application/pdf", "staff_orders_info.pdf");
        }

        [HttpGet("JSON/StaffOrdersInfo")]
        public ActionResult GetStaffOrdersInfoJson(
            [FromQuery] DateOnly date)
        {
            var staffOrdersInfos =
                statisticsService.GetStaffOrdersInfoByMonth(date)
                    .ToList();

            // Serialize the list of StaffPayrollResult objects to JSON
            var json = JsonSerializer.Serialize(staffOrdersInfos);

            // Convert the JSON string to bytes
            var data = Encoding.UTF8.GetBytes(json);

            // Set content type to JSON and return the data as a file
            return File(data, "application/json", "staff_orders_info.json");
        }

        [HttpGet("XML/Orders")]
        public ActionResult GetOrdersXml(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            var orders =
                Mappers.MapOrderToResponseDto(
                    orderService.GetAllWithFullInfo(
                        o => o.Date >= dateStart && o.Date < dateEnd,
                        asNoTracking: true))
                    .ToList();

            // Create XML serializer for the StaffPayrollResult type
            var serializer = new XmlSerializer(typeof(List<ResponseOrderDto>));

            using MemoryStream stream = new MemoryStream();

            // Serialize the list of StaffPayrollResult objects to XML
            serializer.Serialize(stream, orders);

            // Reset the stream position to the beginning
            stream.Seek(0, SeekOrigin.Begin);

            // Set content type to XML and return the stream as a file
            return File(stream.ToArray(), "application/xml", "orders.xml");
        }

        [HttpGet("JSON/Orders")]
        public ActionResult GetOrdersJson(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            var orders =
                Mappers.MapOrderToResponseDto(
                    orderService.GetAllWithFullInfo(
                        o => o.Date >= dateStart && o.Date < dateEnd,
                        asNoTracking: true))
                    .ToList();

            // Serialize the list of StaffPayrollResult objects to JSON
            var json = JsonSerializer.Serialize(orders);

            // Convert the JSON string to bytes
            var data = Encoding.UTF8.GetBytes(json);

            return File(data, "application/json", "orders.json");
        }

        [HttpGet("CSV/Orders")]
        public IActionResult GetOrdersCsv(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            // Retrieve the orders from your database
            var orders =
                Mappers.MapOrderToResponseDto(
                    orderService.GetAll(
                        o => o.Date >= dateStart && o.Date < dateEnd,
                    asNoTracking: true))
                        .ToList();

            // Construct the CSV header
            var csv = new StringBuilder();
            csv.AppendLine("Order ID,Date,Staff ID,Customer ID,Delivery,Delivery Address ID");

            // Append each order to the CSV
            foreach (var order in orders)
            {
                csv.AppendLine($"{order.OrderId},{order.Date},{order.StaffId},{order.CustomerId},{order.Delivery},{order.DeliveryAddressId}");
            }

            // Convert the CSV string to bytes using UTF-8 encoding
            var data = Encoding.UTF8.GetBytes(csv.ToString());

            // Set content type to CSV and return the data as a file
            return File(data, "text/csv", "orders.csv");
        }

        [HttpGet("CSV/Logs")]
        [Authorize(Roles = $"{UserRoleNames.Manager}, {UserRoleNames.Admin}")]
        public IActionResult GetLogsCsv(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            // Retrieve the orders from your database
            var logs =
                    logService.GetAll(
                        x => x.TimeStamp >= dateStart && x.TimeStamp < dateEnd,
                    asNoTracking: true)
                        .ToList();

            // Construct the CSV header
            var csv = new StringBuilder();
            csv.AppendLine("Id,Message,MessageTemplate,Level,TimeStamp,Exception,Properties");

            // Append each order to the CSV
            foreach (var log in logs)
            {
                csv.AppendLine($"{log.Id},{log.Message},{log.MessageTemplate},{log.Level},{log.TimeStamp},{log.Exception},{log.Properties}");
            }

            // Convert the CSV string to bytes using UTF-8 encoding
            var data = Encoding.UTF8.GetBytes(csv.ToString());

            // Set content type to CSV and return the data as a file
            return File(data, "text/csv", "logs.csv");
        }
        
        [HttpGet("JSON/Logs")]
        [Authorize(Roles = $"{UserRoleNames.Manager}, {UserRoleNames.Admin}")]
        public IActionResult GetLogsJson(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            // Retrieve the orders from your database
            var logs =
                    logService.GetAll(
                        x => x.TimeStamp >= dateStart && x.TimeStamp < dateEnd,
                    asNoTracking: true)
                        .ToList();

            if(logs.Count >= 58000) throw new OverflowException("Too much log entry objects. Enter shorter time period.");

            var json = JsonSerializer.Serialize(logs);

            // Convert the JSON string to bytes
            var data = Encoding.UTF8.GetBytes(json);

            return File(data, "application/json", "logs.json");
        }

        [HttpGet("XML/Logs")]
        [Authorize(Roles = $"{UserRoleNames.Manager}, {UserRoleNames.Admin}")]
        public IActionResult GetLogsXml(
            [FromQuery] DateTime dateStart,
            [FromQuery] DateTime dateEnd)
        {
            // Retrieve the orders from your database
            var logs =
                    logService.GetAll(
                        x => x.TimeStamp >= dateStart && x.TimeStamp < dateEnd,
                    asNoTracking: true)
                        .ToList();

            // Create XML serializer for the StaffPayrollResult type
            var serializer = new XmlSerializer(typeof(List<Log>));

            using MemoryStream stream = new MemoryStream();

            // Serialize the list of StaffPayrollResult objects to XML
            serializer.Serialize(stream, logs);

            // Reset the stream position to the beginning
            stream.Seek(0, SeekOrigin.Begin);

            // Set content type to XML and return the stream as a file
            return File(stream.ToArray(), "application/xml", "logs.xml");
        }
    }
}