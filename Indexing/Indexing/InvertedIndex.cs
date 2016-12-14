using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NLog;
using StemmerNet;
using static System.StringComparison;

namespace Indexing
{
    [Serializable]
    public class InvertedIndex
    {
        //private readonly Dictionary<string, HashSet<int>> _index = new Dictionary<string, HashSet<int>>();
        private Dictionary<string, Dictionary<int,int>> _index = new Dictionary<string, Dictionary<int,int>>();

        public Dictionary<string, Dictionary<int, int>> Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private readonly Regex _findWords = new Regex(@"[A-Za-z]+");
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IStemmer _stemmer = new StemmerNet.EnglishStemmer();

        public int WordCount => _index.Count;

        /// <summary>
        /// Adds to the inverted index
        /// </summary>
        /// <param name="text">Text of the document to add</param>
        /// <param name="docId">document identifier number</param>
        public void Add(string text, int docId)
        {
            var words = _findWords.Matches(text);
  

            foreach (var word in (from Match wordMatch in words select wordMatch.Value.ToLower())
                .Where(word => !StopWords.StopWordsList.Contains(word)).Select(word => _stemmer.Stem(word)))
            {
                if (!_index.ContainsKey(word))
                {
                    //_index[word] = new HashSet<int>();
                    _index[word] = new Dictionary<int, int>();
                }

                if (!_index[word].ContainsKey(docId))
                {
                    _index[word].Add(docId, 1);
                    //  _logger.Trace($"Added word {word}");
                }
                else
                {
                   _index[word][docId]++; // Increment word count
                  //  _logger.Trace($"Incremented word {word}");
                }
            }
        }

        public int GetTermFrequency(string word, int docId)
        {
            return _index[word][docId];
        }

        public void Add(string[] text, int docId)
        {
            foreach (var word in text.Select(item => _findWords.Matches(item))
                .SelectMany(words => (from Match wordMatch in words select wordMatch.Value.ToLower())
                .Where(word => !StopWords.StopWordsList.Contains(word)).Select(word => _stemmer.Stem(word))))
            {
                if (!_index.ContainsKey(word))
                {
                    _index[word] = new Dictionary<int, int>();
                }

                if (!_index[word].ContainsKey(docId))
                {
                    _index[word].Add(docId, 1);
                    //  _logger.Trace($"Added word {word}");
                }
                else
                {
                    _index[word][docId]++; // Increment word count
                    //  _logger.Trace($"Incremented word {word}");
                }
            }
        }

        /// <summary>
        /// Searches the index for words in the search query
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>List of document IDs that match the query</returns>
        public List<int> SearchWord(string query)
        {
            var words = _findWords.Matches(query);
            IEnumerable<int> rtn = null;

            for (var i = 0; i < words?.Count; i++)
            {
                var word = words[i].Value;
                if (_index.ContainsKey(word))
                {
                    rtn = rtn?.Intersect(_index[word].Keys) ?? _index[word].Keys;
                }
                else
                {
                    return new List<int>();
                }
            }

            return rtn?.ToList() ?? new List<int>();
        }

        /// <summary>
        /// Searches the index for words in the search query
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>List of document IDs that match the query</returns>
        public Dictionary<int,int> Search(string query)
        {
            return _index.ContainsKey(query) ? _index[query] : new Dictionary<int, int>();
        }

        /// <summary>
        /// Finds a list of documents with terms that contain a given substring
        /// </summary>
        /// <param name="word">Word term to search</param>
        /// <param name="limit"></param>
        /// <param name="maxStringSize"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<int>> ContainsSubString(string word, int limit = 100, int maxStringSize = 15)
        {
            var wordMatches = _index
                .Where(pv => pv.Key.IndexOf(word, Ordinal) > -1 && pv.Key.Length <= maxStringSize)
                .Select(i => i.Key)
                .Take(limit);

            return wordMatches.Select(w => _index[w].Keys.ToList().Take(limit));
        }
    }

}