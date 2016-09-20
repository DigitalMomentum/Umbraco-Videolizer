using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Videolizer {
    public class VideolizerVideo {
        public enum VideoTypes {
            Unknown,
            YouTube,
            Vimeo
        }
       // public static readonly Regex VimeoVideoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
       // public static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
        public static readonly Regex VimeoVideoRegex = new Regex(@"(?:https?:\/\/)?(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|)(\d+)(?:$|\/|\?)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public static readonly Regex VimeoAlternateVideoRegex = new Regex(@"(?:https?:\/\/)?(www\.|player\.)?vimeo.com\/(\d+)\/(.+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public static readonly Regex YoutubeVideoRegex = new Regex(@"(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$", RegexOptions.IgnoreCase);


        public VideolizerVideo() { }

        /// <summary>
        /// Initialises the object with a given Video URL
        /// </summary>
        /// <param name="VideoUrl">The URL to a Youtube or Vimeo Video</param>
        public VideolizerVideo(string VideoUrl) {
            this.url = VideoUrl;
            string vidId = ytVidId(VideoUrl);
            if (vidId != null) {
                //Its a Youtube Clip.
                this.id = vidId;
                this.type = VideoTypes.YouTube;
                this.embedUrl = "https://www.youtube.com/embed/" + vidId;
            }else {
                vidId = vimeoVidId(VideoUrl);
                if (vidId != null) {
                    //Its a Youtube Clip.
                    this.id = vidId;
                    this.type = VideoTypes.Vimeo;
                    this.embedUrl = "//player.vimeo.com/video/" + vidId;
                }
            }
        }

        /// <summary>
        /// The Raw URL entered by the content editor
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// The Video ID from Youtube/Vimeo ETC
        /// </summary>
        public string id { get; set; }


        /// <summary>
        /// The URL for the Embed Iframe
        /// </summary>
        public string embedUrl { get; set; }



        /// <summary>
        /// The name of the video Service
        /// </summary>
        public VideoTypes type { get; set; }


        /// <summary>
        /// Returns the HTML for the Video Embed in the form of an iFrame
        /// </summary>
        /// <param name="width">Width of the Video (px or %)</param>
        /// <param name="height">Height of the Video (px or %)</param>
        /// <returns>Iframe Embed to play the video</returns>
        public HtmlString GetSimpleEmbed(string width, string height) {
            return GetSimpleEmbed(width, height, null, null);
        }

        /// <summary>
        /// Returns the HTML for the Video Embed in the form of an iFrame
        /// </summary>
        /// <param name="width">Width of the Video (px or %)</param>
        /// <param name="height">Height of the Video (px or %)</param>
        /// <param name="cssClasses">CSS Clases to add to the iframe</param>
        /// <returns>Iframe Embed to play the video</returns>
        public HtmlString GetSimpleEmbed(string width, string height, string cssClasses) {
            return GetSimpleEmbed(width, height, cssClasses, null);
        }

        /// <summary>
        /// Returns the HTML for the Video Embed in the form of an iFrame
        /// </summary>
        /// <param name="width">Width of the Video (px or %)</param>
        /// <param name="height">Height of the Video (px or %)</param>
        /// <param name="cssClasses">CSS Clases to add to the iframe</param>
        /// <param name="styles">Styles to add to the iframe</param>
        /// <returns>Iframe Embed to play the video</returns>
        public HtmlString GetSimpleEmbed(string width, string height, string cssClasses, string styles) {
            if(embedUrl == null) {
                return new HtmlString("");
            }
            string classStr = "";
            if(cssClasses != null) {
                classStr = string.Format(" class=\"{0}\"", cssClasses);
            }
            string styleStr = "";
            if (styleStr != null) {
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

    

        /// <summary>
        /// Gets the Video ID from a Youtube URL
        /// </summary>
        /// <param name="url">URL to the video</param>
        /// <returns>Video ID or Null</returns>
        public string ytVidId(string url) {
            Match vidMatch = YoutubeVideoRegex.Match(url);
            if (vidMatch.Success) {
                return vidMatch.Groups[1].Value;
            }
            return null;
        }


        /// <summary>
        /// Gets the Video ID from a Vimeo URL
        /// </summary>
        /// <param name="url">URL to the video</param>
        /// <returns>Video ID or Null</returns>
        public string vimeoVidId(string url) {
            Match vidMatch = VimeoVideoRegex.Match(url);
            if (vidMatch.Success) {
                return vidMatch.Groups[3].Value;
            }
            vidMatch = VimeoAlternateVideoRegex.Match(url);
            if (vidMatch.Success) {
                return vidMatch.Groups[2].Value;
            }
            return null;
        }
    }
}