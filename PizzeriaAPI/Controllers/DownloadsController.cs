using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Pizzeria.Domain.Identity.Roles;
using Pizzeria.Domain.Services.Statistics;

namespace PizzeriaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = UserRoleNames.Manager)]
    public class DownloadsController(IStatisticsService statisticsService) : ControllerBase
    {
        [HttpGet("PDF/StaffPayroll")]
        public ActionResult GetStaffPayrollPdf(
        [FromQuery] DateTime dateStart,
        [FromQuery] DateTime dateEnd)
        {
            var staffPayrollResults = statisticsService.CalculateStaffPayroll(dateStart, dateEnd).ToList();
            
            if(staffPayrollResults.IsNullOrEmpty())
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
    }
}