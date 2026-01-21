using Microsoft.Identity.Client;
using Microsoft.Maui.ApplicationModel;

namespace SentireChat.Services;

public class AuthService
{
    private readonly IPublicClientApplication _pca;

    private static readonly string[] Scopes =
    {
        AppConfig.ApiScope
    };

    public AuthService()
    {
        _pca = PublicClientApplicationBuilder
            .Create(AppConfig.ClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, AppConfig.TenantId)
            .WithRedirectUri(AppConfig.RedirectUri)
            .Build();
    }

    public async Task<string> GetTokenAsync(bool interactive)
    {
        try
        {
            var accounts = await _pca.GetAccountsAsync();
            var first = accounts.FirstOrDefault();

            if (!interactive && first != null)
            {
                var silent = await _pca
                    .AcquireTokenSilent(Scopes, first)
                    .ExecuteAsync();

                return silent.AccessToken;
            }

            var builder = _pca.AcquireTokenInteractive(Scopes);

#if ANDROID
            builder = builder.WithParentActivityOrWindow(
                Platform.CurrentActivity);
#endif

            var result = await builder.ExecuteAsync();
            return result.AccessToken;
        }
        catch (MsalUiRequiredException)
        {
            // força login interativo
            var result = await _pca
                .AcquireTokenInteractive(Scopes)
#if ANDROID
                .WithParentActivityOrWindow(Platform.CurrentActivity)
#endif
                .ExecuteAsync();

            return result.AccessToken;
        }
    }

    public async Task<bool> IsLoggedAsync()
    {
        var accounts = await _pca.GetAccountsAsync();
        return accounts.Any();
    }

    public async Task LogoutAsync()
    {
        var accounts = await _pca.GetAccountsAsync();
        foreach (var acc in accounts)
            await _pca.RemoveAsync(acc);
    }
}