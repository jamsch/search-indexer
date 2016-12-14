using System.Windows;
using System.Windows.Forms;
using SearchGUI_WPF_MvvM.Model;
using SearchGUI_WPF_MvvM.ViewModel;

namespace SearchGUI_WPF_MvvM
{
    /// <summary>
    /// Description for IndexingView.
    /// </summary>
    public partial class IndexingView : Window
    {
        /// <summary>
        /// Initializes a new instance of the IndexingView class.
        /// </summary>
        public IndexingView()
        {
            InitializeComponent();
        }
        private void BtnBrowseDict_Click(object sender, RoutedEventArgs e)
        {
            var fb = new FolderBrowserDialog();
            if (fb.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            TextBoxDictLocation.Text = fb.SelectedPath;

            if (!string.IsNullOrEmpty(TextBoxIndexLocation.Text))
                BtnLoadDictAndIndex.IsEnabled = true;
        }

        private void BtnBrowseIndex_Click(object sender, RoutedEventArgs e)
        {
            var fb = new FolderBrowserDialog();
            if (fb.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            TextBoxIndexLocation.Text = fb.SelectedPath;

            if (!string.IsNullOrEmpty(TextBoxDictLocation.Text))
                BtnLoadDictAndIndex.IsEnabled = true;

        }

        private void BtnLoadDictAndIndex_Click(object sender, RoutedEventArgs e)
        {
  
        }
    }
}