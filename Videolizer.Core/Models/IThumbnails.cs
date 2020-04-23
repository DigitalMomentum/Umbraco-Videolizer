using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Models
{
    public interface IThumbnails
    {
        [JsonProperty("default")]
        string Default { get;  }

        [JsonProperty("highQuality")]
        string HighQuality { get;  }

        [JsonProperty("mediumQuality")]
        string MediumQuality { get;  }

        [JsonProperty("standardDef")]
        string StandardDef { get;  }

        [JsonProperty("maximum")]
        string Maximum { get;  }
    }
}
