using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentUtils;
using NLog;
using NLog.LayoutRenderers.Wrappers;
using StemmerNet;

namespace SearchUtils
{
    /// <summary>
    /// Handles search requests done on inverted index and sorts the results
    /// </summary>
    public class SearchQuery : ISearch
    {
        // Private fields
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        //private SQLiteConnection _dbConnection;

        // Properties
        public int QueryLimit { get; set; } = 200;
        public bool FilterByDate { get; set; }
        public bool SearchFileContents { get; set; }
        private List<SearchResult> SearchResults { get; set; }
        public DocumentManager DocumentManager { get; set; }

        private readonly IStemmer _stemmer = new EnglishStemmer();

        private Action<string> _searchWorker;

        public SearchQuery()
        {
            /*
            try
            {
                _dbConnection = new SQLiteConnection("Data Source=Words.db;Version=3;");
                _dbConnection.Open();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            */
        }

        public List<SearchResult> GetSearchResults()
        {
            return SearchResults;
        } 

        /// <summary>
        /// Performs a threaded asynchronous search query
        /// </summary>
        /// <param name="callback">Callback function to call once complete</param>
        /// <param name="query">Search query</param>
        /// <param name="objectstate">Async object state</param>
        public void DoSearch(AsyncCallback callback, string query = "", object objectstate = null)
        {
            _searchWorker = Search;
            Action<string> doSearch = _searchWorker;
            _searchWorker.BeginInvoke(query, delegate(IAsyncResult result)
            {
                callback(result);
                try
                {
                    doSearch.EndInvoke(result);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                };
            }, objectstate);
        }

        /// <summary>
        /// Performs a search query on the inverted index
        /// </summary>
        /// <returns>A list of Document Ids</returns>
        public void Search(string query)
        {
            // Start stopwatch
            var s = new Stopwatch();
            s.Start();

            // Split query by words
            var words = query.Split();
           
            IList<int> lastIntersect = null;
            
            var docScores = new Dictionary<int, double>();
            var matchedWords = new Dictionary<int, Dictionary<string, int>>();

            foreach (var word in words)
            {
                // Skip searching for stopwords
                if (Indexing.StopWords.StopWordsList.Contains(word)) continue;

                var stemmedWord = _stemmer.Stem(word);

                var wordMatches = DocumentManager.Index.Search(stemmedWord);

                if (lastIntersect == null) lastIntersect = wordMatches.Keys.ToList();

                var wordKeys = wordMatches.Keys.ToList();

                // Calculate IDF for word (Inverse Document Frequency)
                var idf = Math.Log((DocumentManager.DocumentCount / (1 + (double) wordKeys.Count())));
               
                // Add TF-IDF score and additional 0.05 to intersecting document words (close in proximity)
                foreach (var docId in lastIntersect.Intersect(wordKeys))
                {
                   var tfidf = (wordMatches[docId] / (double)DocumentManager.GetDocument(docId).NumTerms) * idf;
                   docScores[docId] = docScores.ContainsKey(docId) ? docScores[docId] + tfidf + 0.05 : tfidf + 0.05;
                    // Add word to search result
                    if (!matchedWords.ContainsKey(docId)) matchedWords[docId] = new Dictionary<string, int>();
                    matchedWords[docId].Add(stemmedWord, wordMatches[docId]);
                    // _logger.Debug($"Document: {docId}, Word Frequency: {wordMatches[docId]}, Terms: {DocumentManager.GetDocument(docId).NumTerms}, IDF: {idf}, Score: {tfidf}  ");
                }

                // Add TF-IDF score if last word does not intersect
                foreach (var docId in wordKeys.Except(lastIntersect))
                {
                    var tfidf = (wordMatches[docId] / (double)DocumentManager.GetDocument(docId).NumTerms) * idf;
                    docScores[docId] = docScores.ContainsKey(docId) ? docScores[docId] + tfidf : tfidf;
                    // Add word to search result
                    if (!matchedWords.ContainsKey(docId)) matchedWords[docId] = new Dictionary<string, int>();
                    matchedWords[docId].Add(stemmedWord, wordMatches[docId]);
                    // _logger.Debug($"Document: {docId}, Word Frequency: {wordMatches[docId]}, Terms: {DocumentManager.GetDocument(docId).NumTerms}, IDF: {idf}, Score: {tfidf}  ");
                }
                // Change last intersect to current intersect
                lastIntersect = wordKeys;
            }

            // Order document score list and take query limit
            var sortedDict = docScores.OrderByDescending(x => x.Value).Take(QueryLimit);

            s.Stop();

            // Set List SearchResults<SearchResult> 
            SearchResults = (
                from doc in sortedDict
                let documentInfo = DocumentManager.GetDocument(doc.Key)
                select new SearchResult()
                {
                    FileLocation = documentInfo.FileInfo.DirectoryName,
                    FileName = documentInfo.FileInfo.Name,
                    DocId = documentInfo.DocId,
                    Score = doc.Value,
                    MatchedWordsDict = matchedWords[doc.Key],
                }).ToList();

            _logger.Debug($"[Search] ({s.ElapsedMilliseconds} ms) \"{query}\". " +
                          $"Documents: {SearchResults.Count}.", 1);
        }

    
    }
}
