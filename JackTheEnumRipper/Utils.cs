using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTheEnumRipper
{
    public class Utils
    {
        public static IEnumerable<string> GetAvailableFormats()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IEnumWriter).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t => t.Name.Replace("Writer", "").ToLower());
        }
    }
}
