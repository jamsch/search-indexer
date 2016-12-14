using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentUtils
{
    [Serializable]
    public class Document
    {
        public int DocId { get; set; }
        public string FileContents { get; set; }
        public FileInfo FileInfo { get; set; }
        public int NumTerms { get; set; } = 1;

    }
}
