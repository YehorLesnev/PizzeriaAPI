namespace Pizzeria.Domain.Dto.StatisticsDto
{
    public record StaffPayrollResult
    {
        public Guid StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Payroll { get; set; }
    }
}
