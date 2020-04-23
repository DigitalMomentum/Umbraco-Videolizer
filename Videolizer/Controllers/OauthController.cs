using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using umbraco.cms.businesslogic.datatype;
using Umbraco.Core;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Videolizer.Core;
using Videolizer.Core.Models;
using Videolizer.Helpers;

namespace Videolizer.SurfaceControllers
{
    [Umbraco.Web.Mvc.PluginController("Videolizer")]
    public class OauthController : UmbracoAuthorizedController
    {

        // /umbraco/backoffice/Plugins/Videolizer/Controllers/OAuth/YouTube
        public ActionResult YouTube()
        {
            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);

            var YT_AppKeys = settings.Get(SettingsHelper.SettingTypes.YT_AppKeys);
            if (YT_AppKeys != null)
            {
                var appKeys = YT_AppKeys.GetValueAsType<ProviderAppDetails>();
                var clientId = appKeys.ClientId;
                var clientSecret = appKeys.ClientSecret;

                ViewData.Add("clientId", clientId);
                ViewData.Add("clientSecret", clientSecret);
            }

            return View("~/App_Plugins/Videolizer/Views/Oauth/YouTube.cshtml");
        }




        // /umbraco/backoffice/Plugins/Videolizer/Controllers/Oauth/InitYT
        public void InitYT(string clientId, string clientSecret)
        {
            var appDetails = new ProviderAppDetails()
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);
            settings.Set(SettingsHelper.SettingTypes.YT_AppKeys, appDetails);

            Videolizer.Core.Auth myAuth = new Videolizer.Core.Auth(Enums.ProviderType.YouTube, appDetails);

            myAuth.RedirectToProviderAuth($"{Request.Url.GetLeftPart(UriPartial.Authority)}/umbraco/backoffice/Plugins/Videolizer/Controllers/Oauth/YouTubeCode", "https://www.googleapis.com/auth/youtube.readonly,https://www.googleapis.com/auth/youtube.upload".Split(','), null);
        }



        // /umbraco/videolizer/OauthYT/InitYT?clientId=1234
        public ActionResult YouTubeCode(string code)
        {
            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);
            ProviderAppDetails appKeys = settings.Get(SettingsHelper.SettingTypes.YT_AppKeys).GetValueAsType<ProviderAppDetails>();

            Videolizer.Core.Auth myAuth = new Videolizer.Core.Auth(Enums.ProviderType.YouTube, appKeys);

            TokenSet tokenSet = myAuth.GetAccessTokenFromAuthCode(code, $"{Request.Url.GetLeftPart(UriPartial.Authority)}/umbraco/backoffice/Plugins/Videolizer/Controllers/Oauth/YouTubeCode");

            settings.Set(SettingsHelper.SettingTypes.YT_TokenSet, tokenSet);

            return View("~/App_Plugins/Videolizer/Views/Oauth/OAuthCode.cshtml");

        }


        // /umbraco/backoffice/Plugins/Videolizer/Controllers/OAuth/Vimeo
        public ActionResult Vimeo()
        {
            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);

            var Vimeo_AppKeys = settings.Get(SettingsHelper.SettingTypes.Vimeo_AppKeys);
            if (Vimeo_AppKeys != null)
            {
                var appKeys = Vimeo_AppKeys.GetValueAsType<ProviderAppDetails>();
                var clientId = appKeys.ClientId;
                var clientSecret = appKeys.ClientSecret;

                ViewData.Add("clientId", clientId);
                ViewData.Add("clientSecret", clientSecret);
            }

            return View("~/App_Plugins/Videolizer/Views/Oauth/Vimeo.cshtml");
        }


        public void InitVimeo(string clientId, string clientSecret)
        {
            var appDetails = new ProviderAppDetails()
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };



            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);
            settings.Set(SettingsHelper.SettingTypes.Vimeo_AppKeys, appDetails);

            Videolizer.Core.Auth myAuth = new Videolizer.Core.Auth(Enums.ProviderType.Vimeo, appDetails);

            myAuth.RedirectToProviderAuth($"{Request.Url.GetLeftPart(UriPartial.Authority)}/umbraco/backoffice/Plugins/Videolizer/Controllers/Oauth/VimeoCode", "https://www.googleapis.com/auth/youtube.readonly,https://www.googleapis.com/auth/youtube.upload".Split(','), null);
        }



        public ActionResult VimeoCode(string code)
        {
            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);
            ProviderAppDetails appKeys = settings.Get(SettingsHelper.SettingTypes.Vimeo_AppKeys).GetValueAsType<ProviderAppDetails>();

            Videolizer.Core.Auth myAuth = new Videolizer.Core.Auth(Enums.ProviderType.Vimeo, appKeys);

            TokenSet tokenSet = myAuth.GetAccessTokenFromAuthCode(code, $"{Request.Url.GetLeftPart(UriPartial.Authority)}/umbraco/backoffice/Plugins/Videolizer/Controllers/Oauth/VimeoCode");

            settings.Set(SettingsHelper.SettingTypes.Vimeo_TokenSet, tokenSet);

            return View("~/App_Plugins/Videolizer/Views/Oauth/OAuthCode.cshtml");

        }

    }
}