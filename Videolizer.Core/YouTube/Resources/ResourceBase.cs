using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;
using Videolizer.Core.Resources;
using static Videolizer.Core.YouTube.Enums;

namespace Videolizer.Core.YouTube.Resources
{
    public class ResourceBase : IResourceBase
    {
        public static string ApiBaseUrl = "https://www.googleapis.com/youtube/v3/";

        private readonly TokenSet tokenSet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenSet"></param>
        public ResourceBase(TokenSet tokenSet)
        {
            this.tokenSet = tokenSet;
        }


        /// <summary>
        /// Utility function to combine a list of "parts" into a format to pass as a query to Youtube
        /// </summary>
        /// <param name="parts">List of "Parts" to be returned by the API</param>
        /// <returns></returns>
        internal string ConvertPartsToString(List<Parts> parts = null) {

            return (parts == null) ? "snippet" : string.Join(",", parts);

         
        }

        /// <summary>
        /// Generic way to get data as a Type from the YouTube API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string resourcePath, Dictionary<string, string> queryStringData)
        {
            var retVal = await GetString(resourcePath, queryStringData);
            if (retVal == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(retVal);
        }

        /// <summary>
        /// Generic way to get data as an object from the YouTube API
        /// </summary>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns></returns>
        public async Task<object> GetObject(string resourcePath, Dictionary<string, string> queryStringData)
        {
            var retVal = await GetString(resourcePath, queryStringData);
            if(retVal == null)
            {
                return null;
            }
            return JObject.Parse(retVal);
           // return JsonConvert.DeserializeObject(retVal);
        }

        /// <summary>
        /// Generic way to get data as a string from the YouTube API
        /// </summary>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns></returns>
        public async Task<string> GetString(string resourcePath, Dictionary<string, string> queryStringData)
        {
            if(tokenSet.Expires < DateTime.Now)
            {
                return null;
            }

            
            //string querystring = $"?access_token={tokenSet.AccessToken}";
            string querystring = $"";
            if (queryStringData != null)
            {
                foreach (var item in queryStringData)
                {
                    querystring += $"&{item.Key}={item.Value}";
                }
            }

            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {tokenSet.AccessToken}");
                try { 
                var responsedata = await client.DownloadDataTaskAsync($"{ApiBaseUrl}{resourcePath}?{querystring.TrimStart('&')}");
             
                return Encoding.Default.GetString(responsedata);
                }
                catch (WebException e)
                {
                    string responseFromServer = "";
                    if (e.Response != null)
                    {
                        using (WebResponse response = e.Response)
                        {
                            System.IO.Stream dataRs = response.GetResponseStream();
                            using (StreamReader reader = new StreamReader(dataRs))
                            {
                                responseFromServer += reader.ReadToEnd();
                            }
                        }
                    }

                    throw new Exception(responseFromServer, e);
                }
            }
        }
    }
}
