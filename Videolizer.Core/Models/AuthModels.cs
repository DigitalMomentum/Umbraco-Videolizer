using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Models
{
    public class TokenSet
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset Expires { get; set; }

        public void SetExpireDateFromSeconds(long seconds)
        {
            Expires = DateTimeOffset.UtcNow.AddSeconds(seconds - 30);
        }
    }

    public class ProviderAppDetails
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
