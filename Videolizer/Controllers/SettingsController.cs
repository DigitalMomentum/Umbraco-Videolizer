using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Videolizer.Core;
using Videolizer.Core.Models;
using Videolizer.Helpers;

namespace Videolizer.Controllers
{
    [Umbraco.Web.Mvc.PluginController("Videolizer")]
    public class SettingsController : UmbracoAuthorizedController
    {

        // /umbraco/backoffice/Plugins/Videolizer/Controllers/Settings/YouTubeStatus
        public async Task<string> YouTubeStatus()
        {
            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);

            TokenSet accessToken = settings.Get(SettingsHelper.SettingTypes.YT_TokenSet)?.GetValueAsType<TokenSet>();

           

            if (accessToken != null)
            {

                if (accessToken.Expires < DateTime.Now)
                {
                    accessToken = YouTubeHelper.RefreshToken();
                }

                Videolizer.Core.YouTube.Resources.Channels channels = new Core.YouTube.Resources.Channels(accessToken);
                var channelList = await channels.ListMine(new List<Core.YouTube.Enums.Parts>() {
                    Core.YouTube.Enums.Parts.id,
                });
                //Core.Resources.Channels channels = new Core.Resources.Channels(Enums.ProviderType.YouTube, accessToken);
                //var channelList = await channels.ListMine();

                //var bob = vidsTest.data[0].pictures.sizes;
                if (channelList.kind == "youtube#channelListResponse")
                {
                    return "Connected";
                }
                else
                {
                    return "Could Not Connect. Time to re-Authorise";
                }

                //}
            }

            return "Not Configured";
        }



        // /umbraco/backoffice/Plugins/Videolizer/Controllers/Settings/vimeoStatus
        public async Task<string> VimeoStatus()
        {
            SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);

            
            TokenSet accessToken = settings.Get(SettingsHelper.SettingTypes.Vimeo_TokenSet)?.GetValueAsType<TokenSet>();

            if (accessToken != null)
            {

                Videolizer.Core.Vimeo.Resources.Channels channels = new Core.Vimeo.Resources.Channels(accessToken);
                try {
                    var channelList = await channels.ListMine();

                    return "Connected";
                }
                catch { 
                    return "Could Not Connect. Time to re-Authorise";
                }
            }

            return "Not Configured";
        }
    }
}