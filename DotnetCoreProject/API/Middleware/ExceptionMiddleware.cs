using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.env = env;
            this.logger = logger;
            this.next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await this.next(context);
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = this.env.IsDevelopment() 
                    ? new APIException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) 
                    : new APIException(context.Response.StatusCode, "Internal Server Error");
                
                //This to Ensure our response comes back as Json
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);


            }
        }
    }
}