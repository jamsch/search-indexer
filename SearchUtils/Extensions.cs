using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUtils
{
   public static class Extensions
    {
        public static void AppendList<T>(this List<T> list, List<T> otherList)
        {
            foreach (var item in otherList.Where(item => !list.Contains(item)))
            {
                list.Add(item);
            }
        }

        public static void Normalize(this List<double> list, int min, int max)
        {
            for (var i = 0; i < list.Count(); i++)
            {
                list[i] = (list[i] - min) / (max - min);
            }
          
        }

    }
}
