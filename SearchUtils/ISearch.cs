using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SearchUtils
{
    public interface ISearch
    {
        /// <summary>
        /// Gets search results
        /// </summary>
        /// <returns></returns>
        List<SearchResult> GetSearchResults();

        /// <summary>
        /// Performs a threaded asynchronous search query
        /// </summary>
        /// <param name="callback">Callback function to call once the search is complete</param>
        /// <param name="query">Search query</param>
        /// <param name="objectstate">Async object state</param>
        void DoSearch(AsyncCallback callback, string query = "", object objectstate = null);
    }
}
