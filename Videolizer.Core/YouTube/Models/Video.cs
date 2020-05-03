using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;
using Videolizer.Core.YouTube.Converters;

namespace Videolizer.Core.YouTube.Models
{
    public class Video : IVideo
    {
        public string Url {
            get {
                return $"//www.youtube.com/watch?v={Id}";
            }
        }

        [JsonProperty(PropertyName = "id")]
        [JsonConverter(typeof(IdJsonConverter))]
        public string Id {
            get; internal set;
        }



        public string Name {
            get {
                return Snippet.Title;
            }
        }

        public string Description {
            get {
                return Snippet.Description;
            }
        }

        public string EmbedUrl {
            get {
                return $"//www.youtube.com/embed/{Id}";
            }
        }

        private Thumbnails _thumbnails = null;
        public IThumbnails Thumbnails {
            get {
                if (_thumbnails == null)
                {
                    _thumbnails = new Thumbnails(Id);
                }

                return _thumbnails;
            }
        }

        [JsonProperty(PropertyName = "snippet")]
        internal YT_Snippet Snippet { get; set; }

        internal class YT_Snippet {

            [JsonProperty(PropertyName = "publishedAt")]
            public string PublishedAt {
                get;
                internal set;
            }

            [JsonProperty(PropertyName = "title")]
            public string Title {
                get;
                internal set;
            }
            [JsonProperty(PropertyName = "description")]
            public string Description {
                get;
                internal set;
            }


        }

        //[JsonProperty(PropertyName = "id")]
        //internal YT_ID ID_Util { get; set; }


        //[JsonProperty(PropertyName = "id")]
        //[JsonConverter(typeof(IdJsonConverter))]
        //internal string ID_Util { get; set; }

        internal class YT_ID
        {
            [JsonProperty(PropertyName = "kind")]
            public string Kind {
                get;
                internal set;
            }

            [JsonProperty(PropertyName = "videoId")]
            public string VideoId {
                get;
                internal set;
            }

            [JsonProperty(PropertyName = "channelId")]
            public string ChannelId {
                get;
                internal set;
            }

            [JsonProperty(PropertyName = "playlistId")]
            public string PlaylistId {
                get;
                internal set;
            }


            /// <summary>
            /// Youtube makes us jump through hoops just to get the ID. This just checks the type and returns the correct ID
            /// </summary>
            /// <returns></returns>
            public string GetId()
            {
                switch (Kind)
                {
                    case "youtube#video":
                        return VideoId;
                    case "youtube#channel":
                        return ChannelId;
                    case "youtube#playlist":
                        return PlaylistId;
                }

                return null;
            }
        }

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
                Type = VideolizerVideo.VideoTypes.YouTube,
                Url = video.Url,
                Description = video.Description,
                Name = video.Name
            };
        }
    }
}
