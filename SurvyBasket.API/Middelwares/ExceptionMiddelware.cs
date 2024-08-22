using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.API.Middelwares
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddelware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddelware( RequestDelegate next , ILogger<ExceptionMiddelware> logger , IHostEnvironment env  )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
    
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong: {Message}", ex.Message);
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
                };

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

               await  httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        }


    }
}
