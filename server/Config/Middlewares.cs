using Rodnie.API.Middlewares.Exceptions;

namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplication AppConfigureMiddlewares(this WebApplication app) {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<GroupMiddleware>();

            return app;
        }
    }
}
