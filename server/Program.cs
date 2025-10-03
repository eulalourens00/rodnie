using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;

using Rodnie.API.Data;
using Rodnie.API.Repositories;
using Rodnie.API.Profiles;
using Rodnie.API.Services;
using Rodnie.API.Services.JWT;
using Rodnie.API.Enums;
using Rodnie.API.Middlewares;
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

            // регистрация репозиториев
            builder.Services.AddScoped<IPinRepository, PinRepository>();
            builder.Services.AddScoped<IPinService, PinService>(); //от нейронки, он почему-то не видит в подсказках нужное, либо я не вижу

            // регистрация сервисов

            // Запуск приложения
            app.AppRun();
        }
    }
}
