using Rodnie.API.Config;

namespace Rodnie.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            // Добавление ms sql сервера (.env)
            builder.AppConfigureSQLServer();

            // Настройка jwt (.env)
            builder.AppConfigureJWT();

            // Регистрация репозиториев (DI)
            builder.AppRegisterRepositories();

            // Регистрация сервисов (DI)
            builder.AppRegisterServices();

            // Профиля для маппера
            builder.AppConfigureProfiles();
           
            var app = builder.Build();

            // Добавление middlewares
            app.AppConfigureMiddlewares();

            // Запуск приложения
            app.AppRun();
        }
    }
}
