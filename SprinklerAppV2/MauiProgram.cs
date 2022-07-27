using SprinklerAppV2.Services;
using SprinklerAppV2.ViewModels;
using SprinklerAppV2.Views;

namespace SprinklerAppV2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "IconFontTypes");
                });
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IZoneToLocal, ZoneToLocal>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ZoneSetupViewModel>();
            builder.Services.AddSingleton<ZoneSetup>();

            return builder.Build();
        }
    }
}