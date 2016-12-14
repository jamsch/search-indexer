using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Input;
using NLog;

namespace SearchUtils
{
    /// <summary>
    /// Data structure for storing a single search result
    /// </summary>
    public class SearchResult
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public KeyValuePair<string, Icon> FileNameAndIcon { get; set; }
        public string FileLocation { get; set; }
        public string FileCreationTime { get; set; }
        public Icon FileIcon { get; set; }
        public int NumOccurances { get; set; }
        public string SentenceOccurance { get; set; }
        public int DocId { get; set; }
        public double Score { get; set; }
        public Dictionary<string, int> MatchedWordsDict { get; set; }
        public string FileSize { get; set; }
        public string DisplayMatchedWords
        {
            get {
                var returnVal = MatchedWordsDict.Aggregate(
                    string.Empty, (current, word) => current + (word.Key + " (" + word.Value +") , "));
                return returnVal.TrimEnd(',', ' ');
            }
      
        }
    }
}
