using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Videolizer.Core;
using Videolizer.Core.Models;

namespace Videolizer.Helpers
{
    public class YouTubeHelper
    {

        public static TokenSet RefreshToken()
        {
            SettingsHelper settings = new SettingsHelper(ApplicationContext.Current.DatabaseContext.Database);

            ProviderAppDetails appKeys = settings.Get(SettingsHelper.SettingTypes.YT_AppKeys).GetValueAsType<ProviderAppDetails>();
            TokenSet tokenSet = settings.Get(SettingsHelper.SettingTypes.YT_TokenSet).GetValueAsType<TokenSet>();


            Videolizer.Core.Auth myAuth = new Videolizer.Core.Auth(Enums.ProviderType.YouTube, appKeys);

            tokenSet = myAuth.RefreshAccessToken(tokenSet);

            settings.Set(SettingsHelper.SettingTypes.YT_TokenSet, tokenSet);

            return tokenSet;

        }

    }
}