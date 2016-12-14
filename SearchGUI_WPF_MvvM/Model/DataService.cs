using System;

namespace SearchGUI_WPF_MvvM.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = DataItem.Current;
            callback(item, null);
        }
    }
}