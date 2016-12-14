using System;
using DocumentUtils;
using Indexing;
using NLog;

namespace SearchGUI_WPF_MvvM.Model
{
    public class DataItem
    {
        public DocumentManager DManager { get; set; }
        public bool IsUsingIndex { get; set; }
        private static DataItem _active;


        public static DataItem Current => _active ?? (_active = new DataItem());

        private DataItem()
        {
            DManager = new DocumentManager();
            IsUsingIndex = false;
        }
    }
}