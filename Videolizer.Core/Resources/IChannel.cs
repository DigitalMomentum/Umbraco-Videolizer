using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Videolizer.Core.YouTube.Enums;

namespace Videolizer.Core.Resources
{
   public  interface IChannel
    {
        Task<dynamic> ListMine();

        Task<T> ListMine<T>();

        Task<dynamic> ListByUser(string username);

        Task<T> ListByUser<T>(string username);

        Task<T> List<T>(Dictionary<string, string> queryStringData);
        
    }
}
