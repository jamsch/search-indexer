using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NlogViewer
{
    /// <summary>
    /// Interaction logic for NlogViewer.xaml
    /// </summary>
    public partial class NlogViewer : UserControl
    {
        public ObservableCollection<LogEventViewModel> LogEntries { get; private set; }
        public bool IsTargetConfigured { get; private set; }

        [Description("Width of time column in pixels"), Category("Data")]
        [TypeConverter(typeof(LengthConverter))]
        public double TimeWidth { get; set; }

        [Description("Width of Logger column in pixels, or auto if not specified"), Category("Data")]
        [TypeConverter(typeof(LengthConverter))]
        public double LoggerNameWidth { set; get; }

        [Description("Width of Level column in pixels"), Category("Data")]
        [TypeConverter(typeof(LengthConverter))]
        public double LevelWidth { get; set; }
        [Description("Width of Message column in pixels"), Category("Data")]
        [TypeConverter(typeof(LengthConverter))]
        public double MessageWidth { get; set; }
        [Description("Width of Exception column in pixels"), Category("Data")]
        [TypeConverter(typeof(LengthConverter))]
        public double ExceptionWidth { get; set; }

        public double MaxNumLogs { get; set; } = 50;
        [Description("Maximum number of logs"), Category("Data")]
        [TypeConverter(typeof(LengthConverter))]


        public NlogViewer()
        {
            IsTargetConfigured = false;
            LogEntries = new ObservableCollection<LogEventViewModel>();

            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            foreach (var target in NLog.LogManager.Configuration.AllTargets
                .Where(t => t is NlogViewerTarget).Cast<NlogViewerTarget>().Where(target => !IsTargetConfigured))
            {
                Debug.WriteLine(target);
                target.LogReceived += LogReceived;
                IsTargetConfigured = true;
            }
        }

        protected void LogReceived(NLog.Common.AsyncLogEventInfo log)
        {
            var vm = new LogEventViewModel(log.LogEvent);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (LogEntries.Count >= MaxNumLogs)
                    LogEntries.RemoveAt(0);
                
                LogEntries.Add(vm);
            }));
        }
    }
}
