using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Vimeo.Models
{
   
    public class VimeoPaging
    {
        /// <summary>
        /// Next
        /// </summary>
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }

        /// <summary>
        /// Previous
        /// </summary>
        [JsonProperty(PropertyName = "previous")]
        public string Previous { get; set; }

        /// <summary>
        /// First
        /// </summary>
        [JsonProperty(PropertyName = "first")]
        public string First { get; set; }

        /// <summary>
        /// Last
        /// </summary>
        [JsonProperty(PropertyName = "last")]
        public string Last { get; set; }
    }
}
