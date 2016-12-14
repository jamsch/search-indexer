using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SearchGUI_WPF_MvvM.ViewModel;

namespace SearchGUI_WPF_MvvM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Task _logTask;
        CancellationTokenSource _cancelLogTask;
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void BtnOpenSearchWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new SearchView();
             window.Show();
            
        }

        private void BackgroundSending_Checked(object sender, RoutedEventArgs e)
        {
            _cancelLogTask = new CancellationTokenSource();
            var token = _cancelLogTask.Token;
            _logTask = new Task(SendLogs, token, token);
            _logTask.Start();
        }

        private void BackgroundSending_Unchecked(object sender, RoutedEventArgs e)
        {
            _cancelLogTask?.Cancel();
        }

        private static void SendLogs(object obj)
        {
            var ct = (CancellationToken)obj;

            var counter = 0;
            var log = NLog.LogManager.GetLogger("task");

            log.Debug("Background Task started.");

            while (!ct.WaitHandle.WaitOne(2000))
            {
                log.Trace($"Message no {counter++}  {log}  from backgroudtask.");
            }

            log.Debug("Background Task stopped.");
        }
    }
}