using Rodnie.API.Services.JWT;
using Rodnie.API.Services;
using Rodnie.API.Services.Groups;
using Rodnie.API.Services.Groups.GroupService;

namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplicationBuilder AppRegisterServices(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IJWTService, JWTService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IGroupService, GroupService>();

            return builder;
        }
    }
}
