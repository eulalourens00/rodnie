using Rodnie.API.Repositories;
using Rodnie.API.Repositories.Groups;
using Rodnie.API.Repositories.Groups.GroupRepository;

namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplicationBuilder AppRegisterRepositories(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();

            return builder;
        }
    }
}
