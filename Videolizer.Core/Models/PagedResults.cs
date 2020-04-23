using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Models
{
    public class PagedResults<T> : IPagedResults<T>
    {
        public PagedResults(){
            Items = new List<T>();
        }

        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("nextPage")]
        public string NextPage { get; set; }

        [JsonProperty("prevPage")]
        public string PrevPage { get; set; }


    }
}
