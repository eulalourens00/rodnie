using Microsoft.Extensions.Logging;
using rodnie.Platforms.Android.Handlers;

namespace rodnie
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<Microsoft.Maui.Controls.Maps.Map, CustomMapHandler>();
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
