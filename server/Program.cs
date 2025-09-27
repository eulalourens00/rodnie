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

namespace Rodnie.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            Env.Load();
            builder = FillServer(builder);
            builder = FillJWT(builder);

            // Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJWTService, JWTService>();

            // AutoMapper
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<UserProfile>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static WebApplicationBuilder FillServer(WebApplicationBuilder builder) {
            var server = Environment.GetEnvironmentVariable("Server");
            if (string.IsNullOrWhiteSpace(server)) {
                Console.WriteLine("Название MS SQL Сервера не указано\n" +
                    "Создайте файл .env в папке сервера и укажите в нем название сервера\n" +
                    "Пример смотрите в файле .env.example");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            var serverOption = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectionString = serverOption?.Replace("{Server}", server);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            return builder;
        }

        public static WebApplicationBuilder FillJWT(WebApplicationBuilder builder) {
            var jwtKey = Environment.GetEnvironmentVariable("JWTKey");
            if (string.IsNullOrWhiteSpace(jwtKey)) {
                Console.WriteLine("JWT ключ не указан\n" +
                    "Пример смотрите в файле .env.example\n" +
                    "Создать ключ(если есть openssl): openssl rand -base64 32");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            var jwtLifetimeMinutes = int.Parse(Environment.GetEnvironmentVariable("JWTLifetimeMinutes") ?? "30");
            builder.Configuration["JWT:Key"] = jwtKey;
            builder.Configuration["JWT:LifetimeMinutes"] = jwtLifetimeMinutes.ToString();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            builder.Services.AddAuthorization(options => {
                options.AddPolicy("Admin", policy => policy.RequireRole( ((int)RolesEnum.Admin).ToString() ));
            });

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }
}
