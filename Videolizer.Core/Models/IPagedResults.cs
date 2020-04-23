using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videolizer.Core.Models
{
    public interface IPagedResults<T>
    {
        /// <summary>
        /// Returned Content / Data
        /// </summary>
        List<T> Items { get; }

        /// <summary>
        /// Total Number of items in the full query.
        /// Please note that the value is an approximation and may not represent an exact value
        /// </summary>
        int Total { get; set; }


        string NextPage { get;}

        string PrevPage { get; }
    }
}
