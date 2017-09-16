using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Videolizer.Core.Utils {
	public class YouTube {
		public static readonly Regex YoutubeVideoRegex = new Regex(@"(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$", RegexOptions.IgnoreCase);


		/// <summary>
		/// Gets the Video ID from a Youtube URL
		/// </summary>
		/// <param name="url">URL to the video</param>
		/// <returns>Video ID or Null</returns>
		public static string GetVideoId(string url) {
			Match vidMatch = YoutubeVideoRegex.Match(url);
			if (vidMatch.Success) {
				return vidMatch.Groups[1].Value;
			}
			return null;
		}

	}
}
