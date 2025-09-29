using Rodnie.API.Repositories;

namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplicationBuilder AppRegisterRepositories(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }
    }
}
