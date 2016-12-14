using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentUtils
{
    public interface IDocumentManager
    {
        /// <summary>
        /// Gets a specific document by document ID
        /// </summary>
        /// <param name="docId">Document ID</param>
        /// <returns></returns>
        Document GetDocument(int docId);

        /// <summary>
        /// Sets the current Document dictionary to the contents a given binary file
        /// </summary>
        /// <param name="docPath">Path to the document dictionary binary encoded file</param>
        /// <param name="indexPath">Path to the inverted index file</param>
        void LoadFromBinaryFile(string docPath, string indexPath);


        /// <summary>
        /// Saves the current state of the inverted index and document dictionary
        /// </summary>
        /// <param name="docPath">Path to save the document dictionary</param>
        /// <param name="indexPath">Path to save the inverted index</param>
        void FlushToDisk(string docPath, string indexPath);

        /// <summary>
        /// Scans the directory and loads all files to an inverted index and dictionary
        /// </summary>
        /// <param name="dir">Directory folder</param>
        void LoadDirectoryContentsToIndex(string dir);

        /// <summary>
        /// Document count
        /// </summary>
        /// <returns></returns>
        int DocumentCount();
    }
}
