using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;
using Videolizer.Core.Resources;
using static Videolizer.Core.YouTube.Enums;

namespace Videolizer.Core.Vimeo.Resources
{
    public class Channels : ResourceBase, IResourceBase, IChannel
    {
        private readonly string resourceType = "channels";

        /// <summary>
        /// Vimeo Channel API calls
        /// </summary>
        /// <param name="tokenSet">Access tokens for authentication</param>
        public Channels(TokenSet tokenSet) : base(tokenSet)
        {

        }


        /// <summary>
        /// Generic way to get a list of all Channels with custom parameters
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="queryStringData">List of Key Values to pass to the API</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> List<T>(Dictionary<string, string> queryStringData)
        {
            return await Get<T>(resourceType, queryStringData);
        }

        /// <summary>
        /// This method returns all the channels to which the specified user subscribes.
        /// </summary>
        /// <param name="user">Vimeo User ID</param>
        /// <returns></returns>
        public async Task<dynamic> ListByUser(string user)
        {
            throw new NotImplementedException("Vimeo does not support this feature");
        }

        /// <summary>
        /// This method returns all the channels to which the specified user subscribes.
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="user">Vimeo User ID</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> ListByUser<T>(string user)
        {
            throw new NotImplementedException("Vimeo does not support this feature");
        }

        /// <summary>
        /// Gets a list of Channels that your subscribed to
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> ListMine()
        {
            return await GetObject($"me/{resourceType}", null);
        }

        /// <summary>
        /// Gets a list of Channels that your subscribed to
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> ListMine<T>()
        {
            return await Get<T>($"me/resourceType", null);
        }
    }
}
