using Rodnie.API.Profiles;

namespace Rodnie.API.Config {
    public static partial class Config {
        public static WebApplicationBuilder AppConfigureProfiles(this WebApplicationBuilder builder) {
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<UserProfile>();
            });

            return builder;
        }
    }
}
