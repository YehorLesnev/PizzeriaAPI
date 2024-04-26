using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace PizzeriaAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                const int statusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.StatusCode = statusCode;

                var innerExceptionMessage = ex.InnerException == null ? "" : $" Inner exception message: {ex.InnerException.Message}";

                ProblemDetails problemDetails = new()
                {
                    Status = statusCode,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = $"An internal server error has occured: {ex.Message}. {innerExceptionMessage}"
                       
                };

                var json = JsonSerializer.Serialize(problemDetails);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}