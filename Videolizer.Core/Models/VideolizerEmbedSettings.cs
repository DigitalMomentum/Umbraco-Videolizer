using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Videolizer.VideolizerVideo;

namespace Videolizer {
	public class VideolizerEmbedSettings {
	

		/// <summary>
		/// Auto Play the video [Supports: Youtube & Vimeo]
		/// </summary>
		public bool AutoPlay = false;

		/// <summary>
		/// Loop the video [Supports: Youtube & Vimeo]
		/// </summary>
		public bool Loop = false;

		/// <summary>
		/// Shows the Users Avitar Icon (Top right corner  [Supports: Vimeo Only]
		/// </summary>
		public bool Portrait = true;

		/// <summary>
		/// Displays the title of the video [Supports: Vimeo Only]
		/// </summary>
		public bool Title = true;

		/// <summary>
		/// Displays a Byline under the title  [Supports: Vimeo Only]
		/// </summary>
		public bool Byline = true;

		/// <summary>
		/// Skip a given number of seconds into the video [Supports: Youtube Only]
		/// </summary>
		public int StartAt = 0;

		/// <summary>
		/// Shows Related Videos at the end of the video [Supports: Youtube Only]
		/// </summary>
		public bool RelatedVideos = false;

		/// <summary>
		/// Show/Hide the player controls [Supports: Youtube only]
		/// </summary>
		public bool Controls = true;


		public VideolizerEmbedSettings(){ }

		public string GetEmbedQueryString(VideoTypes videoType, string VideoId) {
			string queryString = "?";
			switch(videoType){
				case VideoTypes.Vimeo:

					
					if (!Portrait) {
						queryString += "portrait=0&";//
					}
					if (!Title) {
						queryString += "title=0&";//
					}
					if (!Byline) {
						queryString += "byline=0&";//
					}
					break;
				case VideoTypes.YouTube:

					if (!Title) {
						queryString += "showinfo=0&";//
					}
					if (!RelatedVideos) {
						queryString += "rel=0&";//
					}

					if (Loop) {
						queryString += "playlist="+ VideoId + "&";//
					}
					break;
			}


			//Generic properties for both services
			if (AutoPlay) {
				queryString += "autoplay=1&";//
			}

			if (!Controls) {
				queryString += "controls=0&";//
			}

			if (Loop) {
				queryString += "loop=1&";//
			}

			if (StartAt > 0) {
				queryString += "start=" + StartAt + "&";//
			}

			


			return queryString.TrimEnd('&');
		}
	}
}