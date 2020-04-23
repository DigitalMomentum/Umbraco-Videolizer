using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;
using static Videolizer.Core.Enums;

namespace Videolizer.Core
{
    public class Auth : IAuth
    {

        private static ProviderType _providerType;
        private IAuth baseAuth;


        public Auth(ProviderType providerType, string clientId, string clientSecret)
        {
            _providerType = providerType;
            switch (providerType)
            {
                case ProviderType.YouTube:
                    baseAuth = new YouTube.Auth(clientId, clientSecret);
                    break;
                case ProviderType.Vimeo:
                    baseAuth = new Vimeo.Auth(clientId, clientSecret);
                    break;
            }
        }

        public Auth(ProviderType providerType, ProviderAppDetails providerAppDetails)
        {
            _providerType = providerType;
            switch (providerType)
            {
                case ProviderType.YouTube:
                    baseAuth = new YouTube.Auth(providerAppDetails);
                    break;
                case ProviderType.Vimeo:
                    baseAuth = new Vimeo.Auth(providerAppDetails);
                    break;
            }
        }


        public string GetProviderAuthRedirect(string redirectUrl, string[] scopes, string state)
        {
            return baseAuth.GetProviderAuthRedirect(redirectUrl, scopes, state);
        }

        public void RedirectToProviderAuth(string redirectUrl, string[] scopes, string state)
        {
            baseAuth.RedirectToProviderAuth(redirectUrl, scopes, state);
        }


        public TokenSet GetAccessTokenFromAuthCode(string authCode, string redirectUrl)
        {
            return baseAuth.GetAccessTokenFromAuthCode(authCode, redirectUrl);
        }


        public TokenSet RefreshAccessToken(TokenSet expiredTokenSet)
        {
            return baseAuth.RefreshAccessToken(expiredTokenSet);
        }
    }
}
