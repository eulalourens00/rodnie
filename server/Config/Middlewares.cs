using Rodnie.API.Middlewares;

namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplication AppConfigureMiddlewares(this WebApplication app) {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
