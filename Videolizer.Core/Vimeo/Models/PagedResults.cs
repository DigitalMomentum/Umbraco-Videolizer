using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Vimeo.Models
{
    /// <summary>
    /// Paginated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResults<T> : Core.Models.IPagedResults<T>
    {
        /// <summary>
        /// Content
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public List<T> Items { get; internal set; }

        /// <summary>
        /// Total
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// Page
        /// </summary>
        [JsonProperty(PropertyName = "page")]
        public int Page { get; internal set; }

        /// <summary>
        /// Per page
        /// </summary>
        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; internal set; }

        /// <summary>
        /// Paging
        /// </summary>
        [JsonProperty(PropertyName = "paging")]
        public VimeoPaging Paging { get; internal set; }


        public string NextPage {
            get {
                return Paging.Next;
            }
        }
        public string PrevPage {
            get {
                return Paging.Previous;
            }
        }
    }
}
