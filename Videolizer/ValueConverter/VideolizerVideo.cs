using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Videolizer {
    public class VideolizerVideo {
        public enum VideoTypes {
            Unknown,
            YouTube,
            Vimeo
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
    }
}