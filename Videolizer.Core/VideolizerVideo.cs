using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Videolizer.Core.Utils;

namespace Videolizer{
	public class VideolizerVideo {
		public enum VideoTypes {
			Unknown,
			YouTube,
			Vimeo
		}

		//public static readonly Regex VimeoVideoRegex = new Regex(@"(?:https?:\/\/)?(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|)(\d+)(?:$|\/|\?)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
		//public static readonly Regex VimeoAlternateVideoRegex = new Regex(@"(?:https?:\/\/)?(www\.|player\.)?vimeo.com\/(\d+)\/(.+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
		//public static readonly Regex YoutubeVideoRegex = new Regex(@"(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$", RegexOptions.IgnoreCase);


		public VideolizerVideo() { }

		/// <summary>
		/// Initialises the object with a given Video URL
		/// </summary>
		/// <param name="VideoUrl">The URL to a Youtube or Vimeo Video</param>
		public VideolizerVideo(string VideoUrl) {
			this.Url = VideoUrl;
			string vidId = YouTube.GetVideoId(VideoUrl);
			if (vidId != null) {
				//Its a Youtube Clip.
				this.Id = vidId;
				this.Type = VideoTypes.YouTube;
				this.EmbedUrl = "https://www.youtube.com/embed/" + vidId;
			} else {
				vidId = Vimeo.GetVideoId(VideoUrl);
				if (vidId != null) {
					//Its a Youtube Clip.
					this.Id = vidId;
					this.Type = VideoTypes.Vimeo;
					this.EmbedUrl = "//player.vimeo.com/video/" + vidId;
				}
			}
		}


		/// <summary>
		/// The Raw URL entered by the content editor
		/// </summary>
		public string Url { get; set; }

		[Obsolete("Sorry! Use Url Instead")]
		public string url {
			get {
				return Url;
			}
			set {
				Url = value;
			}
		}

		/// <summary>
		/// The Video ID from Youtube/Vimeo ETC
		/// </summary>
		public string Id { get; set; }

		[Obsolete("Sorry! Use Id Instead")]
		public string id { get { return Id; } set { Id = value; } }


		/// <summary>
		/// The URL for the Embed Iframe
		/// </summary>
		public string EmbedUrl { get; set; }


		[Obsolete("Sorry! Use EmbedUrl Instead")]
		public string embedUrl { get { return EmbedUrl; } set { EmbedUrl = value; } }



		/// <summary>
		/// The name of the video Service
		/// </summary>
		public VideoTypes Type { get; set; }

		[Obsolete("Sorry! Use Type Instead")]
		public VideoTypes type { get { return Type; } set { Type = value; } }


		/// <summary>
		/// Checks to see if the object has a video to display
		/// </summary>
		/// <returns>true if there is a video URL</returns>
		public bool HasVideo() {
			return !string.IsNullOrEmpty(Url);
		}


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
			if (EmbedUrl == null || !HasVideo()) {
				return new HtmlString("");
			}
			string classStr = "";
			if (cssClasses != null) {
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
					EmbedUrl,
					classStr,
					styleStr
				));
		}



		///// <summary>
		///// Gets the Video ID from a Youtube URL
		///// </summary>
		///// <param name="url">URL to the video</param>
		///// <returns>Video ID or Null</returns>
		//public string YouTubeVidId(string url) {
		//	Match vidMatch = YoutubeVideoRegex.Match(url);
		//	if (vidMatch.Success) {
		//		return vidMatch.Groups[1].Value;
		//	}
		//	return null;
		//}

		[Obsolete("Sorry! Use Videolizer.Core.Utils.YouTube.YouTubeVidId Instead")]
		public string ytVidId(string url) {
			return YouTube.GetVideoId(url);
		}


		///// <summary>
		///// Gets the Video ID from a Vimeo URL
		///// </summary>
		///// <param name="url">URL to the video</param>
		///// <returns>Video ID or Null</returns>
		//public string VimeoVidId(string url) {
		//	Match vidMatch = VimeoVideoRegex.Match(url);
		//	if (vidMatch.Success) {
		//		return vidMatch.Groups[3].Value;
		//	}
		//	vidMatch = VimeoAlternateVideoRegex.Match(url);
		//	if (vidMatch.Success) {
		//		return vidMatch.Groups[2].Value;
		//	}
		//	return null;
		//}

		[Obsolete("Sorry! Use Videolizer.Core.Utils.Vimeo.VimeoVidId Instead")]
		public string vimeoVidId(string url) {
			return Vimeo.GetVideoId(url);
		}
	}
}