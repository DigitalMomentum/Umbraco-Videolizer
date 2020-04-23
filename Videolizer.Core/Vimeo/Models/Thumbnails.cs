using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;

namespace Videolizer.Core.Vimeo.Models
{
    public class Thumbnails : IThumbnails
    {



        public Thumbnails(VimeoPictures pictures)
        {
                var sizes = pictures?.Sizes.OrderBy(r => r.Width);
                Default = sizes?.FirstOrDefault()?.Url;
                HighQuality = sizes?.Where(r => r.Width >= 450).FirstOrDefault()?.Url;
                MediumQuality = sizes?.Where(r => r.Width >= 300).FirstOrDefault()?.Url;
                StandardDef = sizes?.Where(r => r.Width >= 640).FirstOrDefault()?.Url;
                Maximum = sizes?.LastOrDefault()?.Url;
            
        }

        public string Default { get; internal set; }


        public string HighQuality { get; internal set; }

        public string MediumQuality { get; internal set; }

        public string StandardDef { get; internal set; }

        public string Maximum { get; internal set; }
    }
}