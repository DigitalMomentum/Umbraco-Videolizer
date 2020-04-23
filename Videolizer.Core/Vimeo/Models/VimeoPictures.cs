using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Vimeo.Models
{
    public class VimeoPictures
    {

        [JsonProperty(PropertyName = "sizes")]
        public IEnumerable<VimeoPicture_Size> Sizes { get; internal set; }

       
    }

    public class VimeoPicture_Size
    {
        [JsonProperty(PropertyName = "height")]
        public int Height { get; internal set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; internal set; }

        [JsonProperty(PropertyName = "Link")]
        public string Url { get; internal set; }

        [JsonProperty(PropertyName = "link_with_play_button")]
        public string UrlWithPlayButton { get; internal set; }
    }

}
