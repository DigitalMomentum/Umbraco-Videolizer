using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Models
{
    public interface IVideo
    {

		/// <summary>
		/// The Raw URL entered by the content editor
		/// </summary>
		string Url { get; }


		/// <summary>
		/// The Video ID from Youtube/Vimeo ETC
		/// </summary>
		string Id { get;  }

		string Name { get; }
		string Description { get; }


		/// <summary>
		/// The URL for the Embed Iframe
		/// </summary>
		string EmbedUrl { get;}

		IThumbnails Thumbnails { get; }

	}
}
