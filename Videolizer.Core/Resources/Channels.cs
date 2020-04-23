using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;
using static Videolizer.Core.Enums;
using static Videolizer.Core.YouTube.Enums;

namespace Videolizer.Core.Resources
{

    public class Channels : ResourceBase, IResourceBase, IChannel

    {
        private readonly IChannel providerClass;


        /// <summary>
        /// Channel API Access
        /// </summary>
        /// <param name="providerType">Youtube or Vimeo</param>
        /// <param name="tokenSet">Access tokens</param>
        public Channels(ProviderType providerType, TokenSet tokenSet): base()
        {
            switch (providerType)
            {
                case ProviderType.Vimeo:
                    Vimeo.Resources.Channels vimeoProviderClass = new Vimeo.Resources.Channels(tokenSet);
                    providerClass = vimeoProviderClass;
                    resourceBaseClass = vimeoProviderClass;
                    break;
                default:
                    YouTube.Resources.Channels ytProviderClass = new YouTube.Resources.Channels(tokenSet);
                    providerClass = ytProviderClass;
                    resourceBaseClass = ytProviderClass;
                    break;
            }
            
        }

        /// <summary>
        /// YT: Lists all your channels. Vimeo: Gets all channels that you are subscribed to
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> ListMine()
        {
            return await providerClass.ListMine();
        }

        /// <summary>
        /// YT: Lists all your channels. Vimeo: Gets all channels that you are subscribed to
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <returns></returns>
        public async Task<T> ListMine<T>()
        {
            return await providerClass.ListMine<T>();
        }


        /// <summary>
        /// Gets a list of channels for a user.
        /// </summary>
        /// <param name="user">Youtube Username or Vimeo User ID</param>
        /// <returns></returns>
        public async Task<dynamic> ListByUser(string user)
        {
            return await providerClass.ListByUser<dynamic>(user);
        }

        /// <summary>
        /// Gets a list of channels for a user.
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="user">Youtube Username or Vimeo User ID</param>
        /// <returns></returns>
        public async Task<T> ListByUser<T>(string user)
        {
            return await providerClass.ListByUser<T>(user);
        }

        /// <summary>
        /// Generic way to get a list of all Channels with custom parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryStringData">List of Key Values to pass to the API </param>
        /// <returns></returns>
        public async Task<T> List<T>(Dictionary<string, string> queryStringData)
        {
            return await providerClass.List<T>(queryStringData);
        }


    }
}