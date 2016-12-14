using System;
using SearchGUI_WPF_MvvM.Model;

namespace SearchGUI_WPF_MvvM.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = DataItem.Current; //"Welcome to MVVM Light [design]"
            callback(item, null);
        }
    }
}