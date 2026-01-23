using Microsoft.Extensions.Logging;
using SentireChat.Pages;
using SentireChat.Services;
using SentireChat.ViewModels;

namespace SentireChat;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Auth
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddTransient<AuthHeaderHandler>();

        // Http + ApiClient
        builder.Services.AddHttpClient<ApiClient>(client =>
        {
            client.BaseAddress = new Uri(AppConfig.ApiBaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .AddHttpMessageHandler<AuthHeaderHandler>();

        // ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<ConversationsViewModel>();
        builder.Services.AddTransient<MessagesViewModel>();

        // Pages
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddTransient<ConversationsPage>();
        builder.Services.AddTransient<MessagesPage>();

        // Shell
        builder.Services.AddSingleton<AppShell>();

        return builder.Build();
    }
}