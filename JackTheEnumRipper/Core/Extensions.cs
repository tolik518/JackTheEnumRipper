using System;
using System.Linq;

namespace JackTheEnumRipper.Core
{
    public static class Extensions
    {
        public static string Repeat(this string source, int times)
        {
            return string.Concat(Enumerable.Repeat(source, times));
        }
    }
}
