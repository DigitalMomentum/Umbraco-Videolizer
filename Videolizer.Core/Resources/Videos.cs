using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;
using static Videolizer.Core.Enums;

namespace Videolizer.Core.Resources
{
    public class Videos : ResourceBase, IResourceBase, IVideos
    {
        private readonly IVideos providerClass;

        public Videos(ProviderType providerType, TokenSet tokenSet) : base() {
            switch (providerType)
            {
                case ProviderType.Vimeo:
                    Vimeo.Resources.Videos vimeoProviderClass = new Vimeo.Resources.Videos(tokenSet);
                    providerClass = vimeoProviderClass;
                    resourceBaseClass = vimeoProviderClass;
                    break;
                default:
                    YouTube.Resources.Videos ytProviderClass = new YouTube.Resources.Videos(tokenSet);
                    providerClass = ytProviderClass;
                    resourceBaseClass = ytProviderClass;
                    break;
            }
        }

        public enum SortOrder
        {
            Date,
            Rating, //Or Vimeo Likes
            Relevance,
            Title,
            Views
        }


        public async Task<PagedResults<VideolizerVideo>> List(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            return await providerClass.List(query, sortOrder, maxResultsPerPage, page);
        }

        public async Task<T> List<T>(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            return await providerClass.List<T>(query, sortOrder, maxResultsPerPage, page);
        }

        public async Task<T> List<T>(Dictionary<string, string> queryStringData)
        {
            return await providerClass.List<T>(queryStringData);
        }

        public async Task<dynamic> ListAsDynamic(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null)
        {
            return await providerClass.ListAsDynamic(query, sortOrder, maxResultsPerPage, page);
        }

        public async Task<PagedResults<VideolizerVideo>> ListMine(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            return await providerClass.ListMine(query, sortOrder, maxResultsPerPage, page, embedable);
        }

        public async Task<T> ListMine<T>(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            return await providerClass.ListMine<T>(query, sortOrder, maxResultsPerPage, page, embedable);
        }

        public async Task<dynamic> ListMineAsDynamic(string query, SortOrder sortOrder = SortOrder.Relevance, int maxResultsPerPage = 50, string page = null, bool embedable = false)
        {
            return await providerClass.ListMineAsDynamic(query, sortOrder, maxResultsPerPage, page, embedable);
        }

       
    }
}
