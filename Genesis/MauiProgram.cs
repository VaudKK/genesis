using Genesis.Service;
using Genesis.ViewModels;
using Microsoft.Extensions.Logging;

namespace Genesis
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
                });

            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<LogInService>();

            builder.Services.AddSingleton<LogIn>();
            builder.Services.AddSingleton<LoginViewModel>();

            return builder.Build();
        }
    }
}
