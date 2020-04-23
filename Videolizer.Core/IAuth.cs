using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;
using static Videolizer.Core.Enums;

namespace Videolizer.Core
{
    interface IAuth
    {

        string GetProviderAuthRedirect(string redirectUrl, string[] scopes, string state);

        void RedirectToProviderAuth(string redirectUrl, string[] scopes, string state);

        TokenSet GetAccessTokenFromAuthCode(string authCode, string redirectUrl);

        TokenSet RefreshAccessToken(TokenSet expiredTokenSet);
    }
}
