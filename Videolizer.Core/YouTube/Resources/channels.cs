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

namespace Videolizer.Core.YouTube.Resources
{
    public class Channels : ResourceBase, IResourceBase, IChannel
    {
        private readonly string resourceType = "channels";

        /// <summary>
        /// Youtube Channel API calls
        /// </summary>
        /// <param name="tokenSet">Access tokens for authentication</param>
        public Channels(TokenSet tokenSet) : base(tokenSet)
        {

        }


        /// <summary>
        /// Gets all Channels that you are subscribed to
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> ListMine()
        {
            return await ListMine<dynamic>(null);
        }


        /// <summary>
        /// Gets all Channels that you are subscribed to
        /// </summary>
        /// <param name="parts">The "Parts" of data that you want returned</param>
        /// <returns></returns>
        public async Task<dynamic> ListMine(List<Parts> parts)
        {
            return await ListMine<dynamic>(parts);
        }


        /// <summary>
        /// Gets all Channels that you are subscribed to
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <returns></returns>
        public async Task<T> ListMine<T>() {
            return await ListMine<T>(null);
        }

        /// <summary>
        /// Gets all Channels that you are subscribed to
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="parts">The "Parts" of data that you want returned</param>
        /// <returns></returns>
        public async Task<T> ListMine<T>(List<Parts> parts)
        {
          
            var queryparams = new Dictionary<string, string>() {
                { "part", ConvertPartsToString(parts) },
                { "mine", "true" },
            };


            return await List<T>(queryparams);
        }

        /// <summary>
        /// Gets a list of channels by the given user
        /// </summary>
        /// <param name="user">The youtube Username</param>
        /// <returns></returns>
        public async Task<dynamic> ListByUser(string user)
        {
            return await ListByUser(user, null);
        }

        /// <summary>
        /// Gets a list of channels by the given user
        /// </summary>
        /// <param name="user">The youtube Username</param>
        /// <param name="parts">The "Parts" of data that you want returned</param>
        /// <returns></returns>
        public async Task<dynamic> ListByUser(string user, List<Parts> parts)
        {
            return await ListByUser<dynamic>(user, parts);
        }

        /// <summary>
        /// Gets a list of channels by the given user
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="user">The youtube Username</param>
        /// <returns></returns>
        public async Task<T> ListByUser<T>(string user)
        {
            return await ListByUser<T>(user, null);
        }

        /// <summary>
        /// Gets a list of channels by the given user
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="user">The youtube Username</param>
        /// <param name="parts">The "Parts" of data that you want returned</param>
        /// <returns></returns>
        public async Task<T> ListByUser<T>(string user, List<Parts> parts)
        {

            var queryparams = new Dictionary<string, string>() {
                { "part", ConvertPartsToString(parts) },
                { "forUsername", user },
            };


            return await List<T>(queryparams);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="queryStringData"></param>
        /// <returns></returns>
        public async Task<T> List<T>(Dictionary<string, string> queryStringData)
        {
            return await Get<T>(resourceType, queryStringData);
        }

       


    }
}
