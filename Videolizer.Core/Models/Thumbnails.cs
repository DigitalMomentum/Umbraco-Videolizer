using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Models
{
    public class Thumbnails : IThumbnails
    {
        public string Default { get; set; }

        public string HighQuality { get; set; }

        public string MediumQuality { get; set; }

        public string StandardDef { get; set; }

        public string Maximum { get; set; }
    }
}
