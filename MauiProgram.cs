using Microsoft.Extensions.Logging;
using SentireChat.Pages;
using SentireChat.Services;

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

        // ===== Services (DI) =====
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddTransient<AuthHeaderHandler>();

        builder.Services.AddHttpClient<ApiClient>(client =>
        {
            client.BaseAddress = new Uri(AppConfig.ApiBaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        })
        .AddHttpMessageHandler<AuthHeaderHandler>();

        // ===== Pages =====
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<ConversationsPage>();
        builder.Services.AddTransient<MessagesPage>();

        // Se você ainda estiver usando MainPage do template, pode registrar também:
        // builder.Services.AddSingleton<MainPage>();

        // ===== Shell =====
        builder.Services.AddSingleton<AppShell>();

        return builder.Build();
    }
}
