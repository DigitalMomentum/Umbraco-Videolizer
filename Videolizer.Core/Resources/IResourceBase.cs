using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Resources
{
    interface IResourceBase
    {
        Task<object> GetObject(string resourcePath, Dictionary<string, string> queryParams);
        Task<string> GetString(string resourcePath, Dictionary<string, string> queryParams);
        Task<T> Get<T>(string resourcePath, Dictionary<string, string> queryParams);
    }
}
