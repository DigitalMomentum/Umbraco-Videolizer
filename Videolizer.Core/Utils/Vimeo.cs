using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Videolizer.Core.Utils {
	public class Vimeo{

		public static readonly Regex VimeoVideoRegex = new Regex(@"(?:https?:\/\/)?(?:www\.|player\.)?vimeo.com\/(?:channels\/(?:\w+\/)?|groups\/([^\/]*)\/videos\/|album\/(\d+)\/video\/|video\/|)(\d+)(?:$|\/|\?)?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
		public static readonly Regex VimeoAlternateVideoRegex = new Regex(@"(?:https?:\/\/)?(www\.|player\.)?vimeo.com\/(\d+)\/(.+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);

		/// <summary>
		/// Gets the Video ID from a Vimeo URL
		/// </summary>
		/// <param name="url">URL to the video</param>
		/// <returns>Video ID or Null</returns>
		public static string GetVideoId(string url) {
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
