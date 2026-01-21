using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Services
{
    public record AuthResult(bool Success, string? AccessToken = null, string? Error = null);

}
