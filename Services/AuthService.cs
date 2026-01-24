using Microsoft.Identity.Client;
using Microsoft.Maui.ApplicationModel;

namespace SentireChat.Services;

public class AuthService : IAuthService
{
    private readonly IPublicClientApplication _pca;

    private static readonly string[] Scopes = { AppConfig.ApiScope };

    public AuthService()
    {
        _pca = PublicClientApplicationBuilder
            .Create(AppConfig.ClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, AppConfig.TenantId)
            .WithRedirectUri(AppConfig.RedirectUri)
            .Build();
    }

    public async Task<AuthResult> TrySilentAsync()
    {
        try
        {
            var account = (await _pca.GetAccountsAsync()).FirstOrDefault();
            if (account is null)
                return new AuthResult(false, Error: "Sem conta logada.");

            var silent = await _pca.AcquireTokenSilent(Scopes, account).ExecuteAsync();
            return new AuthResult(true, silent.AccessToken);
        }
        catch (MsalUiRequiredException)
        {
            return new AuthResult(false, Error: "Login interativo necessário.");
        }
        catch (Exception ex)
        {
            return new AuthResult(false, Error: ex.Message);
        }
    }

    public async Task<AuthResult> LoginAsync()
    {
        try
        {
            var builder = _pca.AcquireTokenInteractive(Scopes);

#if ANDROID
            builder = builder.WithParentActivityOrWindow(Platform.CurrentActivity);
#endif

#if DEBUG
            // Evita abrir Custom Tabs/Browser e reduzir chance do VS perder o debugger
            builder = builder.WithUseEmbeddedWebView(true);
#endif

            var result = await builder.ExecuteAsync();
            return new AuthResult(true, result.AccessToken);
        }
        catch (Exception ex)
        {
            return new AuthResult(false, Error: ex.Message);
        }
    }

    public async Task<bool> IsLoggedAsync()
        => (await _pca.GetAccountsAsync()).Any();

    public async Task LogoutAsync()
    {
        var accounts = await _pca.GetAccountsAsync();
        foreach (var acc in accounts)
            await _pca.RemoveAsync(acc);
    }
}