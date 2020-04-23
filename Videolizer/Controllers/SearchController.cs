using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Umbraco.Web.WebApi;
using Videolizer.Core;
using Videolizer.Core.Models;
using Videolizer.Helpers;

namespace Videolizer.Controllers
{
    [IsBackOffice]
    public class SearchController : UmbracoAuthorizedApiController
    {


        ///Umbraco/BackOffice/Api/Search/Query
        [HttpGet]
        public async Task<PagedResults<VideolizerVideo>> Query(string query, Enums.ProviderType providerType, bool myVideos = false, string channelId = null, int itemsPerPage=10)
        {
            SettingsHelper.SettingTypes settingTypes = (providerType == Enums.ProviderType.YouTube) ? SettingsHelper.SettingTypes.YT_TokenSet : SettingsHelper.SettingTypes.Vimeo_TokenSet;

             SettingsHelper settings = new SettingsHelper(ApplicationContext.DatabaseContext.Database);

            TokenSet accessToken = settings.Get(settingTypes).GetValueAsType<TokenSet>();

            if (providerType == Enums.ProviderType.YouTube)
            {
                accessToken = settings.RefreshYTTokenIfExpired(accessToken);
            }

            if (accessToken != null)
            {
                Core.Resources.Videos videos = new Core.Resources.Videos(providerType, accessToken);

                //If no query, then we want the latest rather then Relevance
                Core.Resources.Videos.SortOrder sortOrder = (string.IsNullOrEmpty(query)) ? Core.Resources.Videos.SortOrder.Date : Core.Resources.Videos.SortOrder.Relevance;

                if (myVideos)
                {
                    return await videos.ListMine(query, sortOrder, itemsPerPage);
                }
                else
                {
                    return await videos.List(query, sortOrder, itemsPerPage);
                }

            }

            return null;
        }


   
    }
}