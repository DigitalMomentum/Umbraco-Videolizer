using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Resources;
using Videolizer.Core.Vimeo.Models;
using static Videolizer.Core.Resources.Videos;
using static Videolizer.Core.YouTube.Enums;

namespace Videolizer.Core.Vimeo.Resources
{
    public class Videos : ResourceBase, IVideos, IResourceBase
    {
        private readonly string resourceType = "videos";

        public Videos(Core.Models.TokenSet tokenSet) : base(tokenSet)
        {

        }

        /// <summary>
        /// Converts the sortOrder enum into the Vimeo Specific sort string
        /// </summary>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <returns></returns>
        public string SortOrderToString(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Rating:
                    return "likes ";
                case SortOrder.Relevance:
                    return "relevant";
                case SortOrder.Title:
                    return "alphabetical";
                case SortOrder.Views:
                    return "plays";
                default:
                    return sortOrder.ToString().ToLower();
            }
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">Pass the Page number</param>
        /// <returns></returns>
        public async Task<dynamic> ListAsDynamic(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            return await List<dynamic>(query, sortOrder, maxResultsPerPage, page);
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">Pass the Page number</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> List<T>(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            var queryparams = new Dictionary<string, string>() {
                { "sort", SortOrderToString(sortOrder) },
                { "per_page", maxResultsPerPage.ToString() },
            };

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryparams.Add("query", query);
            }

            if (!string.IsNullOrWhiteSpace(page))
            {
                queryparams.Add("page", page);
            }

            return await Get<T>(resourceType, queryparams);
        }

        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="queryStringData">List of Key Values to pass to the API</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> List<T>(Dictionary<string, string> queryParams)
        {
            return await Get<T>(resourceType, queryParams);
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
            return await ListMine<dynamic>(query, sortOrder, maxResultsPerPage, page, embedable);
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
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        public async Task<T> ListMine<T>(string query, Core.Resources.Videos.SortOrder sortOrder = Core.Resources.Videos.SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            string srt = SortOrderToString(sortOrder);
            if (srt == "relevant")
            {
                //Cant search you own videos with this sort order
                srt = "default";
            }

            var queryparams = new Dictionary<string, string>() {
                { "sort", srt },
                { "per_page", maxResultsPerPage.ToString() },
            };

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryparams.Add("query", query);
            }

            if (!string.IsNullOrWhiteSpace(page))
            {
                queryparams.Add("page", page);
            }

            if (embedable)
            {
                queryparams.Add("filter", "embeddable");
                queryparams.Add("filter_embeddable", "true");
            }

            return await Get<T>($"me/{resourceType}", queryparams);
        }


        public async Task<Core.Models.PagedResults<VideolizerVideo>> ListMine(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            var videoQuery = await ListMine<PagedResults<Video>>(query, sortOrder, maxResultsPerPage, page, embedable);

            Core.Models.PagedResults<VideolizerVideo> retVal = new Core.Models.PagedResults<VideolizerVideo>()
            {
                Total = videoQuery.Total,
                NextPage = videoQuery.NextPage,
                PrevPage = videoQuery.PrevPage,
                Items = videoQuery.Items.Select(Video.MapToVideolizerVideo).ToList()
            };

            return retVal;
        }

        public async Task<Core.Models.PagedResults<VideolizerVideo>> List(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            var videoQuery = await List<PagedResults<Video>>(query, sortOrder, maxResultsPerPage, page);

            Core.Models.PagedResults<VideolizerVideo> retVal = new Core.Models.PagedResults<VideolizerVideo>()
            {
                Total = videoQuery.Total,
                NextPage = videoQuery.NextPage,
                PrevPage = videoQuery.PrevPage,
                Items = videoQuery.Items.Select(Video.MapToVideolizerVideo).ToList()
            };



            return retVal;
        }
    }
}
