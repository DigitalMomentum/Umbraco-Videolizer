using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videolizer.Core.Models;

namespace Videolizer.Core.YouTube.Models
{
    public class Thumbnails : IThumbnails
    {

        private string videoId = "";

        public Thumbnails(string videoId)
        {
            this.videoId = videoId;
        }

        public string Default {
            get {
                return $"https://img.youtube.com/vi/{videoId}/default.jpg";
            }
        }

        public string HighQuality {
            get {
                return $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";
            }
        }

        public string MediumQuality {
            get {
                return $"https://img.youtube.com/vi/{videoId}/mqdefault.jpg";
            }
        }

        public string StandardDef {
            get {
                return $"https://img.youtube.com/vi/{videoId}/sddefault.jpg";
            }
        }

        public string Maximum {
            get {
                return $"https://img.youtube.com/vi/{videoId}/maxresdefault.jpg";
            }
        }
    }
}
