using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Videolizer.Core;
using Videolizer.Core.Models;
using Videolizer.Core.Utils;

namespace Videolizer
{
    public class VideolizerVideo : IVideo
    {
        public enum VideoTypes
        {
            Unknown,
            YouTube,
            Vimeo
        }

        public VideolizerVideo() { }

        public VideolizerVideo(string VideoId, VideoTypes type)
        {
            switch (type)
            {
                case VideoTypes.YouTube:
                    this.Id = VideoId;
                    this.Type = VideoTypes.YouTube;
                    this.EmbedUrl = "//www.youtube.com/embed/" + VideoId;
                    break;
                case VideoTypes.Vimeo:
                    this.Id = VideoId;
                    this.Type = VideoTypes.Vimeo;
                    this.EmbedUrl = "//player.vimeo.com/video/" + VideoId;
                    break;
            }
        }

        /// <summary>
        /// Initialises the object with a given Video URL
        /// </summary>
        /// <param name="VideoUrl">The URL to a Youtube or Vimeo Video</param>
        public VideolizerVideo(string VideoUrl)
        {
            this.Url = VideoUrl;
            string vidId = YouTube.GetVideoId(VideoUrl);
            if (vidId != null)
            {
                //Its a Youtube Clip.
                this.Id = vidId;
                this.Type = VideoTypes.YouTube;
                this.EmbedUrl = "//www.youtube.com/embed/" + vidId;
            }
            else
            {
                vidId = Vimeo.GetVideoId(VideoUrl);
                if (vidId != null)
                {
                    //Its a Vimeo Clip.
                    this.Id = vidId;
                    this.Type = VideoTypes.Vimeo;
                    this.EmbedUrl = "//player.vimeo.com/video/" + vidId;
                }
            }
        }



        /// <summary>
        /// The Raw URL entered by the content editor
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }


        /// <summary>
        /// The Video ID from Youtube/Vimeo ETC
        /// </summary>

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The URL for the Embed Iframe
        /// </summary>
        [JsonProperty("embedUrl")]
        public string EmbedUrl { get; set; }


        /// <summary>
        /// The name of the video Service
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public VideoTypes Type { get; set; }

        //[JsonProperty("thumbnails")]
        //public IThumbnails Thumbnails { get; internal set; }

        [JsonProperty("thumbnails")]
        public Thumbnails Thumbnails { get; set; }

        IThumbnails IVideo.Thumbnails {
            get {
                return Thumbnails;
            }
        }


        /// <summary>
        /// Checks to see if the object has a video to display
        /// </summary>
        /// <returns>true if there is a video URL</returns>
        public bool HasVideo()
        {
            return !string.IsNullOrEmpty(Url);
        }


        public string GetEmbedUrl(bool autoPlay = false, bool loop = false, bool showAvitar = true, bool showTitle = true, bool showByLine = false, bool showSugestedVideos = false, bool showPlayerControls = true)
        {
            return this.EmbedUrl;
        }





        /// <summary>
        /// Returns the HTML for the Video Embed in the form of an iFrame
        /// </summary>
        /// <param name="width">Width of the Video (px or %)</param>
        /// <param name="height">Height of the Video (px or %)</param>
        /// <param name="cssClasses">CSS Clases to add to the iframe</param>
        /// <param name="styles">Styles to add to the iframe</param>
        /// <returns>Iframe Embed to play the video or Empty string if no video</returns>
        public HtmlString GetSimpleEmbed(string width, string height, VideolizerEmbedSettings embedSettings = null, string cssClasses = null, string styles = null)
        {
            if (EmbedUrl == null || !HasVideo())
            {
                return new HtmlString("");
            }

            string embedUrl = EmbedUrl;

            if (embedSettings != null)
            {
                embedUrl += embedSettings.GetEmbedQueryString(Type, Id);
            }


            string classStr = "";
            if (cssClasses != null)
            {
                classStr = string.Format(" class=\"{0}\"", cssClasses);
            }
            string styleStr = "";
            if (styleStr != null)
            {
                styleStr = string.Format(" style=\"{0}\"", styles);
            }
            return new HtmlString(string.Format(
                "<iframe width=\"{0}\" height=\"{1}\" src=\"{2}\"{3}{4} frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>",
                    width,
                    height,
                    embedUrl,
                    classStr,
                    styleStr
                ));
        }
    }
}