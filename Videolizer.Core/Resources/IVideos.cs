using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Videolizer.Core.Resources.Videos;

namespace Videolizer.Core.Resources
{
    interface IVideos
    {



        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <returns></returns>
        Task<Core.Models.PagedResults<VideolizerVideo>> List(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null);


        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <returns></returns>
        Task<dynamic> ListAsDynamic(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null);


        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        Task<T> List<T>(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null);


        /// <summary>
        /// Gets a list of Videos
        /// </summary>
        /// <typeparam name="T">Typed class to associate to the returned JSON</typeparam>
        /// <param name="queryStringData">List of Key Values to pass to the API</param>
        /// <returns>Strongly typed object that matches the returned JSON</returns>
        Task<T> List<T>(Dictionary<string, string> queryStringData);


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
        Task<Core.Models.PagedResults<VideolizerVideo>> ListMine(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false);

        /// <summary>
        /// Gets a list of the current users Videos
        /// </summary>
        /// <param name="query">Search query used to search for videos</param>
        /// <param name="sortOrder">The sort order of the videos</param>
        /// <param name="maxResultsPerPage">Maximum results. Also used as items per page</param>
        /// <param name="page">YOUTUBE: Pass the next/prev token to go through pages. VIMEO: Pass the Page number</param>
        /// <param name="embedable">true = videos that can be embedded, false = any video</param>
        /// <returns></returns>
        Task<dynamic> ListMineAsDynamic(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false);

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
        Task<T> ListMine<T>(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false);
    }
}
