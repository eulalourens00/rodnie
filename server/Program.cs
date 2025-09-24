using Microsoft.EntityFrameworkCore;
using Rodnie.API.Data;
using DotNetEnv;

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
            
            var server = Environment.GetEnvironmentVariable("Server");
            if (string.IsNullOrWhiteSpace(server)) {
                Console.WriteLine("Название MS SQL Сервера не указано\n" +
                    "Создайте файл .env в папке сервера и укажите в нем название сервера\n" +
                    "Пример смотрите в файле .env.example");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            var template = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectionString = template.Replace("{Server}", server);
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionString)
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
