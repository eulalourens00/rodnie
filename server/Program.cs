using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using AutoMapper;

using Rodnie.API.Data;
using Rodnie.API.Models;

using Rodnie.API.DTO.Responses;
using Rodnie.API.Repositories;
using Rodnie.API.Profiles;
using Rodnie.API.Services;

namespace Rodnie.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Database context
            var server = Environment.GetEnvironmentVariable("Server");
            if (string.IsNullOrWhiteSpace(server)) {
                Console.WriteLine("Название MS SQL Сервера не указано\n" +
                    "Создайте файл .env в папке сервера и укажите в нем название сервера\n" +
                    "Пример смотрите в файле .env.example");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            var template = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectionString = template?.Replace("{Server}", server);
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionString)
            );

            // Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Services
            builder.Services.AddScoped<IUserService, UserService>();

            // AutoMapper
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<UserProfile>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
