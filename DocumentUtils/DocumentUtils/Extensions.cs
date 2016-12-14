using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentUtils
{
    internal static class Extensions
    {
        public static void Append<T>(this Dictionary<int, T> dictionary, T value)
        {
            dictionary.Add(dictionary.Count, value);
        }
    }
}
