using System.Net.Http.Headers;

namespace SentireChat.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IAuthService _auth;

    public AuthHeaderHandler(IAuthService auth)
    {
        _auth = auth;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var result = await _auth.TrySilentAsync();

        if (result.Success && !string.IsNullOrWhiteSpace(result.AccessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", result.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}