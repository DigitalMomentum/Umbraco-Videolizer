using Newtonsoft.Json;
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
    public class ResourceBase : IResourceBase
    {

        internal IResourceBase resourceBaseClass { get; set; }

        public ResourceBase()
        {
        }

        /// <summary>
        /// Generic way to get data as a Type from the API
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> Get<T>(string resourcePath, Dictionary<string, string> queryStringData)
        {
            return await resourceBaseClass.Get<T>(resourcePath, queryStringData);
        }

        /// <summary>
        /// Generic way to get data as an object from the API
        /// </summary>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns>Data from the API call as an object</returns>
        public async Task<object> GetObject(string resourcePath, Dictionary<string, string> queryStringData)
        {
            return await resourceBaseClass.GetObject(resourcePath, queryStringData);
        }

        /// <summary>
        /// Generic way to get data as a string from the API
        /// </summary>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns>JSON string from the API call</returns>
        public async Task<string> GetString(string resourcePath, Dictionary<string, string> queryStringData)
        {
            return await resourceBaseClass.GetString(resourcePath, queryStringData);
        }




       
    }
}
