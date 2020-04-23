using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;

namespace Videolizer.Core.YouTube.Models
{
    public class PagedResults<T> : Core.Models.IPagedResults<T>
    {


        [JsonProperty(PropertyName = "items")]
        public List<T> Items { get; internal set; }


        public int Total {
            get {
                return PageInfo.TotalResults;
            }
            set {
                if (PageInfo == null)
                {
                    PageInfo = new PageInfoModel();
                }
                PageInfo.TotalResults = value;
            }
        }

        [JsonProperty(PropertyName = "nextPageToken")]
        public string NextPage { get; internal set; }


        [JsonProperty(PropertyName = "prevPageToken")]
        public string PrevPage { get; internal set; }



        //Youtube properties

        [JsonProperty(PropertyName = "pageInfo")]
        internal PageInfoModel PageInfo { get; set; }

        internal class PageInfoModel
        {

            [JsonProperty(PropertyName = "totalResults")]
            public int TotalResults { get; internal set; }


            [JsonProperty(PropertyName = "resultsPerPage")]
            public int ResultsPerPage { get; internal set; }
        }
    }
}
