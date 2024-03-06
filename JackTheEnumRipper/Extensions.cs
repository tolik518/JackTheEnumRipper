using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JackTheEnumRipper
{
    public static class Extensions
    {
        public static T? GetAttribute<T>(this ICustomAttributeProvider assembly, bool inherit = false) where T : Attribute
        {
            return assembly
                .GetCustomAttributes(typeof(T), inherit)
                .OfType<T>()
                .FirstOrDefault();
        }
    }
}
