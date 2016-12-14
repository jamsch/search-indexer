using DocumentUtils;
using GalaSoft.MvvmLight;
using SearchGUI_WPF_MvvM.Model;

namespace SearchGUI_WPF_MvvM.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class IndexingModel : ViewModelBase
    {
        private DocumentManager _dm;
        /// <summary>
        /// Initializes a new instance of the IndexingModel class.
        /// </summary>
        public IndexingModel(IDataService dataService)
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
                });
        }
    }
}