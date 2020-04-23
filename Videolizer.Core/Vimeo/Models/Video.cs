using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;

namespace Videolizer.Core.Vimeo.Models
{
    public class Video : IVideo
    {

        [JsonProperty(PropertyName = "link")]
        public string Url { get; internal set; }


        [JsonProperty(PropertyName = "resource_key")]
        public string Id { get; internal set; }



        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; internal set; }


        public string EmbedUrl {
            get {
                return $"https://player.vimeo.com/video/{Uri.Replace("/videos/", "")}";
            }

        }

        public IThumbnails Thumbnails {
            get {
                return new Thumbnails(Pictures);
            }
        }







        [JsonProperty(PropertyName = "pictures")]
        internal VimeoPictures Pictures {
            get; set;
        }




        //Vimeo Specific Properties



        [JsonProperty(PropertyName = "uri")]
        public string Uri {
            get; set;
        }


        //[JsonProperty(PropertyName = "embed")]
        //internal VimeoEmbedSettings Embed { get; set; }

        //internal class VimeoEmbedSettings
        //{
        //    [JsonProperty(PropertyName = "uri")]
        //    public string Uri { get; set; }
        //}


        public static VideolizerVideo MapToVideolizerVideo(Video video)
        {
            return new VideolizerVideo()
            {
                EmbedUrl = video.EmbedUrl,
                Id = video.Id,
                Thumbnails = new Core.Models.Thumbnails()
                {
                    Default = video.Thumbnails.Default,
                    HighQuality = video.Thumbnails.HighQuality,
                    Maximum = video.Thumbnails.Maximum,
                    MediumQuality = video.Thumbnails.MediumQuality,
                    StandardDef = video.Thumbnails.StandardDef,
                },
                Type = VideolizerVideo.VideoTypes.Vimeo,
                Url = video.Url,
                Description = video.Description,
                Name = video.Name
            };
        }
    }
}
