using Serilog;

namespace PizzeriaAPI.Extensions
{
    public static class LoggingExtensions
    {
        public static void AddSerilog(this WebApplication app)
        {
            // Logger
            Serilog.Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(app.Configuration)
                .CreateLogger();
        }
    }
}
