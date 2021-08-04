using Newtonsoft.Json;
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

namespace Videolizer.Core.Vimeo.Resources
{
    public class ResourceBase : IResourceBase
    {
        public static string ApiBaseUrl = "https://api.vimeo.com/";

        private readonly TokenSet tokenSet;


        public ResourceBase(TokenSet tokenSet)
        {
            this.tokenSet = tokenSet;
        }

        /// <summary>
        /// Generic way to get data as a Type from the Vimeo API
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> Get<T>(string resourcePath, Dictionary<string, string> queryParams)
        {
            var retVal = await GetString(resourcePath, queryParams);
            if (retVal == null)
            {
                return default;
            }


            return JsonConvert.DeserializeObject<T>(retVal);

        }

        /// <summary>
        /// Generic way to get data as an object from the Vimeo API
        /// </summary>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns>Data from the API call as an object</returns>
        public async Task<object> GetObject(string resourcePath, Dictionary<string, string> queryParams)
        {
            var retVal = await GetString(resourcePath, queryParams);
            if (retVal == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject(retVal);
        }

        /// <summary>
        /// Generic way to get data as a string from the Vimeo API
        /// </summary>
        /// <param name="resourcePath">URL Path for the API. e.g. "videos" OR "channels"</param>
        /// <param name="queryStringData">Querystring data to pass to the API</param>
        /// <returns>JSON string from the API call</returns>
        public async Task<string> GetString(string resourcePath, Dictionary<string, string> queryParams)
        {
            if (tokenSet.Expires < DateTime.Now)
            {
                return null;
            }

            string querystring = $"";
            if (queryParams != null)
            {
                foreach (var item in queryParams)
                {
                    querystring += $"&{item.Key}={item.Value}";
                }
            }

			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; //Dont know if I need this?

			using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, $"bearer {tokenSet.AccessToken}");

                try
                {
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
