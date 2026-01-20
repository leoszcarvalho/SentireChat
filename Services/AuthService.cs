using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Services
{
    public class AuthService
    {
        private readonly IPublicClientApplication _pca;

        public AuthService()
        {
            _pca = PublicClientApplicationBuilder
                .Create(AppConfig.ClientId)
                .WithTenantId(AppConfig.TenantId)
                .WithRedirectUri(AppConfig.RedirectUri)
                .Build();
        }

        public async Task<bool> IsLoggedAsync()
            => (await _pca.GetAccountsAsync()).Any();

        public async Task LogoutAsync()
        {
            var accounts = await _pca.GetAccountsAsync();
            foreach (var acc in accounts)
                await _pca.RemoveAsync(acc);
        }

        public async Task<string> GetTokenAsync(bool interactive)
        {
            var scopes = new[] { AppConfig.ApiScope };
            var account = (await _pca.GetAccountsAsync()).FirstOrDefault();

            try
            {
                if (!interactive && account != null)
                {
                    return (await _pca
                        .AcquireTokenSilent(scopes, account)
                        .ExecuteAsync()).AccessToken;
                }
            }
            catch (MsalUiRequiredException) { }

            var builder = _pca.AcquireTokenInteractive(scopes)
                .WithPrompt(Prompt.SelectAccount);

#if ANDROID
        builder = builder.WithParentActivityOrWindow(Platform.CurrentActivity);
#endif

            return (await builder.ExecuteAsync()).AccessToken;
        }
    }
}
