using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DocumentUtils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using SearchGUI_WPF_MvvM.Model;
using SearchUtils;


namespace SearchGUI_WPF_MvvM.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    /// mvvminpc
    public class SearchModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private DocumentManager _dm;
        private readonly ISearch _query;
        private bool _isUsingIndex;

        /// <summary>
        /// Initializes a new instance of the SearchModel class.
        /// </summary>
        public SearchModel(IDataService dataService)
        {
            dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }


                    _dm = item.DManager;
                    _isUsingIndex = item.IsUsingIndex;
                });
            _query = new SearchQuery()
            {
                DocumentManager = _dm,
                FilterByDate = false,
                SearchFileContents = false

            };
            
            _logger.Trace("Search window opened");
        }

        /// <summary>
        /// The <see cref="SearchQuery" /> property's name.
        /// </summary>
        public const string SearchQueryPropertyName = "SearchQuery";

        private string _searchQuery;

        /// <summary>
        /// Sets and gets the SearchQuery property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }

            set
            {
                if (_searchQuery == value)
                {
                    return;
                }

                _searchQuery = value;
                RaisePropertyChanged(SearchQueryPropertyName);
                Search();
            }
        }



        /// <summary>
        /// The <see cref="FolderLocation" /> property's name.
        /// </summary>
        public const string FolderLocationPropertyName = "FolderLocation";

        private string _folderLocation;

        /// <summary>
        /// Sets and gets the FolderLocation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FolderLocation
        {
            get
            {
                return _folderLocation;
            }

            set
            {
                if (_folderLocation == value)
                {
                    return;
                }

                _folderLocation = value;
                RaisePropertyChanged(FolderLocationPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="SearchResults" /> property's name.
        /// </summary>
        public const string SearchResultsPropertyName = "SearchResults";

        private List<SearchResult> _searchResults = new List<SearchResult>();

        /// <summary>
        /// Sets and gets the SearchResults property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<SearchResult> SearchResults
        {
            get
            { 
                return _searchResults;
            }

            set
            {
                if (_searchResults == value)
                {
                    return;
                }

                _searchResults = value;
                RaisePropertyChanged(SearchResultsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LoadDocumentContents" /> property's name.
        /// </summary>
        public const string LoadDocumentContentsPropertyName = "LoadDocumentContents";

        private bool _loadDocumentContents = false;

        /// <summary>
        /// Sets and gets the LoadDocumentContents property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool LoadDocumentContents
        {
            get
            {
                return _loadDocumentContents;
            }

            set
            {
                if (_loadDocumentContents == value)
                {
                    return;
                }

                _loadDocumentContents = value;
                _logger.Debug($"Loading Document Contents: {_loadDocumentContents}");
                RaisePropertyChanged(propertyName: LoadDocumentContentsPropertyName);
            }
        }

        public const string IsUsingIndexPropertyName = "IsUsingIndex";

        public bool IsUsingIndex
        {
            get { return _isUsingIndex; }
            set
            {
                _isUsingIndex = value;
                RaisePropertyChanged(propertyName: IsUsingIndexPropertyName);
            }
        }

        private ICommand _browseFileCommand;
        public ICommand BrowseFileCommand => _browseFileCommand ?? (_browseFileCommand = new RelayCommand(BrowseFile));


        /// <summary>
        /// Opens dialog window for browsing folder directory and indexes once the user selects 'OK'
        /// </summary>
        private void BrowseFile()
        {
            var fb = new FolderBrowserDialog();
            if (fb.ShowDialog() != DialogResult.OK) return;

            FolderLocation = fb.SelectedPath;

            _logger.Debug($"User selected path {fb.SelectedPath}. Starting thread");

            var a = new LockingWithAwait();
            if (!_loadDocumentContents)
            {
                _logger.Debug("Not loading file contents");
                a.LoadDirectoryContentsToIndex(FolderLocation, _dm);
            }
            else
            {
                _logger.Debug("Loading file contents");
                a.LoadDirectoryContentsToIndex(FolderLocation, _dm, true);
            }

        }

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new RelayCommand(Search));

        private object _searchStatus;

        /// <summary>
        /// Local implementation of the Search method. 
        /// Should bring up search results on the GUI
        /// </summary>
        private void Search()
        {
            if (_dm.Index == null) return;
            _searchStatus = new object();
            new Thread(() => _query.DoSearch(CompleteSearch, query: SearchQuery, objectstate: _searchStatus)).Start();
        }

        /// <summary>
        /// Callback accessed from Search()
        /// </summary>
        /// <param name="result"></param>
        private void CompleteSearch(IAsyncResult result)
        {
            SearchResults = _query.GetSearchResults();
        }

    }
    internal class LockingWithAwait
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public async void LoadDirectoryContentsToIndex(string dir, DocumentManager rm, bool loadFileContents = false)
        {
            await Task.Run(() => rm.LoadDirectoryContentsToIndex(dir, loadFileContents));
        }
    }
}