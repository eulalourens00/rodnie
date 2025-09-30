namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplication AppRun(this WebApplication app) {
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();

            return app;
        }
    }
}
