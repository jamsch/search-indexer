using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace SearchGUI_WPF_MvvM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
