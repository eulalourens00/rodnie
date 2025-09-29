using Rodnie.API.Services.JWT;
using Rodnie.API.Services;

namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplicationBuilder AppRegisterServices(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJWTService, JWTService>();

            return builder;
        }
    }
}
