using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.YouTube
{
    public class Enums
    {
        public enum Parts
        {
            auditDetails,
            brandingSettings,
            contentDetails,
            contentOwnerDetails,
            id,
            invideoPromotion,
            localizations,
            snippet,
            statistics,
            status,
            topicDetails
        }

        public enum ResourceTypes
        {
            Channels,
            Search,
            Videos
        }
    }
}
