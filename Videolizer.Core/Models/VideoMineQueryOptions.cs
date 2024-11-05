using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Models
{

    /// <summary>
    /// Additional Query Options 
    /// </summary>
    public class VideoMineQueryOptions
    {
        /// <summary>
        /// Vimeo Only: Filter to a specific folder (Also known as a project in the Vimeo API docs)
        /// </summary>
        public long? FolderId { get; set; } = null;
    }
}
