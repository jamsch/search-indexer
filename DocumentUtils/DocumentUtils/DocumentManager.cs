using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Indexing;
using NLog;


namespace DocumentUtils
{

    public class DocumentManager
    {
        private readonly List<string> _fileFormats = new List<string>() { ".txt", ".html", ".css", ".js", ".py", "" };

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public InvertedIndex Index { get; set; }

        private Dictionary<int, Document> _docDictionary = new Dictionary<int, Document>();

        public Document GetDocument(int docId)
        {
            return _docDictionary[docId];
        }
        public int DocumentCount => _docDictionary.Count;
        public DocumentManager()
        {
            Index = new InvertedIndex();
        }

        /// <summary>
        /// Sets the current Document dictionary to the contents a given binary file
        /// </summary>
        /// <param name="docPath">Path to the document dictionary binary encoded file</param>
        /// <param name="indexPath">Path to the inverted index file</param>
        public void LoadFromBinaryFile(string docPath, string indexPath)
        {
           _docDictionary = Utils.ReadFromBinaryFile<Dictionary<int, Document>>(docPath);
            _logger.Trace($"Loaded document dictionary from {docPath}");
            Index.Index = Utils.ReadFromBinaryFile<Dictionary<string,Dictionary<int,int>>>(indexPath);
            _logger.Trace($"Loaded inverted index from {indexPath}");
        }

        /// <summary>
        /// Saves the current state of the inverted index and document dictionary
        /// </summary>
        /// <param name="docPath">Path to save the document dictionary</param>
        /// <param name="indexPath">Path to save the inverted index</param>
        public void FlushToDisk(string docPath, string indexPath)
        {
            Utils.WriteToBinaryFile(docPath, _docDictionary);
            _logger.Trace($"Saved document dict at {docPath}");
            Utils.WriteToBinaryFile(indexPath, Index.Index);
            _logger.Trace($"Saved Index at {indexPath}");
        }

        /// <summary>
        /// Scans the directory and loads all files to an inverted index and dictionary
        /// </summary>
        /// <param name="dir">Directory folder</param>
        /// <param name="fileFormats">A list of defined file formats to scan contents</param>
        /// <param name="loadFileContents">Loads file contents as well as file name</param>
        public void LoadDirectoryContentsToIndex(string dir, bool loadFileContents = false, List<string> fileFormats = null)
        {

            if (fileFormats == null) fileFormats = _fileFormats;
           
            try
            {
                foreach (var f in Directory.GetFiles(dir))
                {
                    var doc = new Document
                    {
                        FileInfo = new FileInfo(f),
                        DocId = _docDictionary.Count,
                        FileContents = null
                    };

                    _docDictionary.Append(doc);

                    // Add Stemmed file name to inverted index
                       
                    Index.Add(Path.GetFileName(f), _docDictionary.Count - 1);

                    if (!loadFileContents) continue;
                    if (!fileFormats.Contains(Path.GetExtension(f))) continue;
                    var text = File.ReadAllText(f).Split();
                    doc.NumTerms = text.Length;
                    Index.Add(text, _docDictionary.Count - 1);
                    

                }
                foreach (var d in Directory.GetDirectories(dir))
                {
                    LoadDirectoryContentsToIndex(d, loadFileContents, fileFormats);
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

        }

        /// <summary>
        /// Clears index and document dictionary
        /// </summary>
        public void ClearIndex()
        {
            Index = new InvertedIndex();
            _docDictionary = new Dictionary<int, Document>();
            _logger.Trace("Cleared Dictionary and Index");
        }
    }
}
