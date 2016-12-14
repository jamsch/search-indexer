using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using NLog;
using SearchGUI_WPF_MvvM.ViewModel;
using SearchUtils;
using static System.Windows.Forms.DialogResult;
using ListViewItem = System.Windows.Controls.ListViewItem;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using MouseEventHandler = System.Windows.Forms.MouseEventHandler;

namespace SearchGUI_WPF_MvvM
{
    /// <summary>
    /// Description for SearchView.
    /// </summary>
    public partial class SearchView : Window
    {
        /// <summary>
        /// Initializes a new instance of the SearchView class.
        /// </summary>
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly List<string> _openFileTypes = new List<string>()
        {
            ".txt",".jpg",".png",".mp3",".wav"
        };

        public SearchView()
        {
          InitializeComponent();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchResult = ((ListViewItem)sender).Content as SearchResult;
        
            if (searchResult == null) return;

            var filePath = searchResult.FileLocation + "\\" + searchResult.FileName;

            if (!File.Exists(filePath)) return;

            if (_openFileTypes.Contains(Path.GetExtension(filePath)))
            {
                _logger.Trace($"Opening file '{searchResult.FileName}'");
                Process.Start(filePath);
            }
            else
            {
                var args = @"/select, " + filePath;
                _logger.Trace($"Opening Explorer.exe with args '{args}'. File type: {Path.GetExtension(filePath)}");
                Process.Start("explorer.exe", args);
            }
        }



    }
}