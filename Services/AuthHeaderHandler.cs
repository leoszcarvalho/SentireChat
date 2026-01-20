using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace SentireChat.Services
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly AuthService _auth;

        public AuthHeaderHandler(AuthService auth)
        {
            _auth = auth;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _auth.GetTokenAsync(interactive: false);
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
