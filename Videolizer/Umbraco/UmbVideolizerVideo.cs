using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Videolizer.Core;

namespace Videolizer.Umbraco {
	/// <summary>
	/// Extends the VideolizerVideo with video settings for DataType and Grid editor Objects
	/// </summary>
	public class UmbVideolizerVideo : VideolizerVideo {

		public UmbVideolizerVideo():base() {

		}
		public UmbVideolizerVideo(string VideoId, VideoTypes type) : base(VideoId, type) {
		}

		/// <summary>
		/// Initialises the object with a given Video URL
		/// </summary>
		/// <param name="VideoUrl">The URL to a Youtube or Vimeo Video</param>
		public UmbVideolizerVideo(string VideoUrl) : base(VideoUrl) {
		}


		/// <summary>
		/// Stores Configutration settings for the embed Querystring
		/// </summary>
		public VideolizerEmbedSettings EmbedConfig = new VideolizerEmbedSettings();

		/// <summary>
		/// Returns the embed URL  with the embed settings saved in the DataType or Grid Item
		/// </summary>
		/// <returns></returns>
		public string GetEmbedUrl(){
			if(EmbedConfig == null){
				return EmbedUrl;
			}
				return EmbedUrl + EmbedConfig.GetEmbedQueryString(Type, Id);
		}


		/// <summary>
		/// Returns the HTML for the Video Embed in the form of an iFrame with the embed settings saved in the DataType or Grid Item
		/// </summary>
		/// <param name="width">Width of the Video (px or %)</param>
		/// <param name="height">Height of the Video (px or %)</param>
		/// <param name="cssClasses">CSS Clases to add to the iframe</param>
		/// <param name="styles">Iframe Embed to play the video or Empty string if no video</param>
		/// <returns></returns>
		public HtmlString GetSimpleEmbed(string width, string height, string cssClasses = null, string styles = null) {
			return GetSimpleEmbed(width, height, EmbedConfig, cssClasses, styles);
		}


	}
}