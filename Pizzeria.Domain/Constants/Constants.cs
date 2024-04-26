namespace Pizzeria.Domain.Constants
{
    public static class Constants
    {
        public static TimeOnly ShiftStartTime = new(8, 0, 0);
        public static TimeOnly ShiftEndTime = new(22, 0, 0);

        public const string DefaultUserPassword = "Password123!";
    }
}
