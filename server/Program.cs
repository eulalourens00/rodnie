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

            // ���������� ms sql ������� (.env)
            builder.AppConfigureSQLServer();

            // ��������� jwt (.env)
            builder.AppConfigureJWT();

            // ����������� ������������ (DI)
            builder.AppRegisterRepositories();

            // ����������� �������� (DI)
            builder.AppRegisterServices();

            // ������� ��� �������
            builder.AppConfigureProfiles();
           
            var app = builder.Build();

            // ���������� middlewares
            app.AppConfigureMiddlewares();

            // ����������� ������������
            builder.Services.AddScoped<IPinRepository, PinRepository>();
            builder.Services.AddScoped<IPinService, PinService>(); //�� ��������, �� ������-�� �� ����� � ���������� ������, ���� � �� ����

            // ����������� ��������

            // ������ ����������
            app.AppRun();
        }
    }
}
