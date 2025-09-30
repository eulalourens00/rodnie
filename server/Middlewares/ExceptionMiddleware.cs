using Rodnie.API.Exceptions;
using System.Net;
using System.Text.Json;

namespace Rodnie.API.Middlewares {
    public class ExceptionMiddleware {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            } catch (RodnieNotFoundException ex) {
                await HandleExceptionAsync(context, 404, ex.Message);
            } catch (UnauthorizedAccessException ex) {
                await HandleExceptionAsync(context, 401, ex.Message);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, 500, "Internal server error");
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            
            var response = new { Error = message };
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
