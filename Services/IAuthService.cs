using System;
using System.Collections.Generic;
using System.Text;

namespace SentireChat.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync();           // login interativo
        Task<AuthResult> TrySilentAsync();       // tenta silent (sem popup)
        Task LogoutAsync();
        Task<bool> IsLoggedAsync();
    }
}
