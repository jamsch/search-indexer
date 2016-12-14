using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using DocumentUtils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Indexing;
using NLog;
using NLog.Filters;
using SearchGUI_WPF_MvvM.Model;

namespace SearchGUI_WPF_MvvM.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private DocumentManager _dm;
        private bool _isUsingIndex = false;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    _dm = item.DManager;
                    _isUsingIndex = item.IsUsingIndex;
                    // WelcomeTitle = item.Title;
                });
        }


        private ICommand _loadIndexCommand;
        public ICommand LoadIndexCommand => _loadIndexCommand ?? (_loadIndexCommand = new RelayCommand(LoadIndex));

        private void LoadIndex()
        {
            var fb = new OpenFileDialog
            {
                Filter = "Inverted Index File (*.*)|*.*"
            };

            if (fb.ShowDialog() != DialogResult.OK) return;

            var indexPath = fb.InitialDirectory + fb.FileName;

            fb = new OpenFileDialog
            {
                Filter = "Document Dictionary File (*.*)|*.*"
            };
            if (fb.ShowDialog() != DialogResult.OK) return;
            var docPath = fb.InitialDirectory + fb.FileName;

           new Thread(() =>
           {
               _isUsingIndex = true;
               _dm.LoadFromBinaryFile(docPath, indexPath);
               _isUsingIndex = false;
           }).Start();
            
        }

        private ICommand _saveIndexCommand;
        public ICommand SaveIndexCommand => _saveIndexCommand ?? (_saveIndexCommand = new RelayCommand(SaveIndex));

        private void SaveIndex()
        {
            var fb = new SaveFileDialog
            {
                Filter = "Inverted Index File (*.*)|*.*"
            };

            if (fb.ShowDialog() != DialogResult.OK) return;

            var indexPath = fb.InitialDirectory + fb.FileName;

            fb = new SaveFileDialog
            {
                Filter = "Document Dictionary File (*.*)|*.*"
            };
            if (fb.ShowDialog() != DialogResult.OK) return;
            var docPath = fb.InitialDirectory + fb.FileName;

            _dm.FlushToDisk(docPath, indexPath);
        }

        private ICommand _dumpDocumentInfoCommand;
        public ICommand DumpDocumentInfoCommand => _dumpDocumentInfoCommand ?? (_dumpDocumentInfoCommand = new RelayCommand(GetDocumentInfo));

        private void GetDocumentInfo()
        {
            _logger.Debug($"Document count: {_dm.DocumentCount}");
            if (_dm.DocumentCount > 0)
            {
                _logger.Debug($"Last document scanned: {_dm.GetDocument(_dm.DocumentCount-1).FileInfo.Name}");
            }
        }

        private ICommand _clearIndexCommand;
        public ICommand ClearIndexCommand => _clearIndexCommand ?? (_clearIndexCommand = new RelayCommand(_dm.ClearIndex));

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}