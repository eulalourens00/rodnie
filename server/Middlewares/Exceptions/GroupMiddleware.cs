using Rodnie.API.Exceptions;

namespace Rodnie.API.Middlewares.Exceptions {
    public class GroupMiddleware {
        private readonly RequestDelegate next;

        public GroupMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            } catch (LimitExceededException ex) {
                await ExceptionHandler.HandleExceptionAsync(context, 400, ex.Message);
            }
        }
    }
}
