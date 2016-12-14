using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchGUI_WPF_MvvM.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
