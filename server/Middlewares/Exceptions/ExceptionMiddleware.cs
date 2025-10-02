using Rodnie.API.Exceptions;

namespace Rodnie.API.Middlewares.Exceptions {
    public class ExceptionMiddleware {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            }
            catch (NotFoundException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 404, ex.Message);
            }
            catch (UnauthorizedAccessException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 401, ex.Message);
            }
            catch (Exception ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 500, "Internal server error");
            }
        }
    }
}
