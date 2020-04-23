using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Resources;
using Videolizer.Core.YouTube.Models;
using static Videolizer.Core.Resources.Videos;
using static Videolizer.Core.YouTube.Enums;

namespace Videolizer.Core.YouTube.Resources
{
    public class Videos : ResourceBase, IVideos, IResourceBase
    {
        private readonly string resourceType = "search";

        public Videos(Core.Models.TokenSet tokenSet) : base(tokenSet)
        {

        }

        /// <summary>
        /// Converts the sortOrder enum into the YouTube Specific sort string
        /// </summary>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <returns></returns>
        public static string SortOrderToString(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Views:
                    return "viewCount ";
                default:
                    return sortOrder.ToString();
            }
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <returns></returns>
        public async Task<dynamic> ListAsDynamic(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            return await ListAsDynamic(query, null, sortOrder, maxResultsPerPage, page);
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <param name="parts">The "Parts" of data that you want returned</param>
        /// <returns></returns>
        public async Task<dynamic> ListAsDynamic(string query, List<Parts> parts, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            return await List<dynamic>(query, sortOrder, maxResultsPerPage, page, parts);
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <returns>Typed class to associate to the returned JSON</returns>
        public async Task<T> List<T>(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            return await List<T>(query, sortOrder, maxResultsPerPage, page, null);
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <param name="parts">The "Parts" of data that you want returned</param>
        /// <returns>Typed class to associate to the returned JSON</returns>
        public async Task<T> List<T>(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, List<Parts> parts = null)
        {
            var queryparams = new Dictionary<string, string>() {
                { "order", SortOrderToString(sortOrder) },
                { "part", ConvertPartsToString(parts) },
                { "maxResults", maxResultsPerPage.ToString() },
            };

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryparams.Add("q", query);
            }

            if (!string.IsNullOrWhiteSpace(page))
            {
                queryparams.Add("pageToken", page);
            }



            return await  Get<T>(resourceType, queryparams);
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="queryStringData">List of Key Values to pass to the API</param>
        /// <returns>Typed class to associate to the returned JSON</returns>
        public async Task<T> List<T>(Dictionary<string, string> queryStringData)
        {
            return await Get<T>(resourceType, queryStringData);
        }


        /// <summary>
        /// Gets a list of the current users Videos
        /// </summary>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <param name="embedable">true = videos that can be embedded, false = any video</param>
        /// <returns></returns>
        public async Task<dynamic> ListMineAsDynamic(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            return await ListMine<dynamic>(query, sortOrder, maxResultsPerPage, page, embedable, null);
        }

      

        /// <summary>
        /// Gets a list of the current users Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <param name="embedable">true = videos that can be embedded, false = any video</param>
        /// <returns>Typed class to associate to the returned JSON</returns>
        public async Task<T> ListMine<T>(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            return await ListMine<T>(query, sortOrder, maxResultsPerPage, page, embedable, null);
        }
        /// <summary>
        /// Gets a list of the current users Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <param name="embedable">true = videos that can be embedded, false = any video</param>
        /// <param name="parts">true = videos that can be embedded, false = any video</param>
        /// <returns>Typed class to associate to the returned JSON</returns>
        public async Task<T> ListMine<T>(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false, List<Parts> parts = null)
        {
            var queryparams = new Dictionary<string, string>() {
                { "forMine", "true" },
                { "order", SortOrderToString(sortOrder) },
                { "part", ConvertPartsToString(parts) },
                { "maxResults", maxResultsPerPage.ToString() },
                { "type", "video" },
            };

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryparams.Add("q", query);
            }

            if (!string.IsNullOrWhiteSpace(page))
            {
                queryparams.Add("pageToken", page);
            }

            if (embedable)
            {
                queryparams.Add("videoEmbeddable", "true");
            }

            //if(typeof(T) == typeof(object))
            //{
            //    object retVal = await GetObject(resourceType, queryparams);
            //    return (T)retVal;
            //}

            return await Get<T>(resourceType, queryparams);
        }

        public async Task<Core.Models.PagedResults<VideolizerVideo>> List(string query, SortOrder sortOrder, int maxResultsPerPage, string page)
        {
            var videoQuery = await List<PagedResults<Video>>(query, sortOrder, maxResultsPerPage, page);

            Core.Models.PagedResults<VideolizerVideo> retVal = new Core.Models.PagedResults<VideolizerVideo>()
            {
                NextPage = videoQuery.NextPage,
                PrevPage = videoQuery.PrevPage,
                Total = videoQuery.Total,
                Items = videoQuery.Items.Select(Video.MapToVideolizerVideo).ToList()
            };

           

            return retVal;
        }

        public async Task<Core.Models.PagedResults<VideolizerVideo>> ListMine(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            var videoQuery = await ListMine<PagedResults<Video>>(query, sortOrder, maxResultsPerPage, page, embedable);

            Core.Models.PagedResults<VideolizerVideo> retVal = new Core.Models.PagedResults<VideolizerVideo>()
            {
                NextPage = videoQuery.NextPage,
                PrevPage = videoQuery.PrevPage,
                Total = videoQuery.Total,
                Items = videoQuery.Items.Select(Video.MapToVideolizerVideo).ToList()
            };

           

            return retVal;
        }
    }
}
