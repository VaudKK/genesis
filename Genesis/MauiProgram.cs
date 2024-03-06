using Genesis.Pages;
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
            builder.Services.AddSingleton<ContributionService>();

            builder.Services.AddSingleton<LogInPage>();
            builder.Services.AddSingleton<LoginViewModel>();


            builder.Services.AddSingleton<ContributionListPage>();
            builder.Services.AddSingleton<ContributionViewModel>();

            builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();


            return builder.Build();
        }
    }
}
