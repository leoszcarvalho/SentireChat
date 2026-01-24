using System.Net.Http.Headers;

namespace SentireChat.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IAuthService _auth;

    private string? _cachedToken;
    private DateTimeOffset _cachedTokenValidUntilUtc = DateTimeOffset.MinValue;

    public AuthHeaderHandler(IAuthService auth) => _auth = auth;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // usa cache se ainda estiver válido (ex: 20 min)
        if (!string.IsNullOrWhiteSpace(_cachedToken) &&
            _cachedTokenValidUntilUtc > DateTimeOffset.UtcNow.AddMinutes(1))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _cachedToken);
            return await base.SendAsync(request, cancellationToken);
        }

        var result = await _auth.TrySilentAsync();

        if (result.Success && !string.IsNullOrWhiteSpace(result.AccessToken))
        {
            _cachedToken = result.AccessToken;
            _cachedTokenValidUntilUtc = DateTimeOffset.UtcNow.AddMinutes(20);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _cachedToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}