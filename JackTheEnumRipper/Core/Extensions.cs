using System;
using System.Linq;
using System.Text;

namespace JackTheEnumRipper.Core
{
    public static class Extensions
    {
        public static string Repeat(this string source, int times)
        {
            return string.Concat(Enumerable.Repeat(source, times));
        }

        public static bool IsValidEncoding(this Encoding encoding, string name)
        {
            try
            {
                _ = Encoding.GetEncoding(name);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
