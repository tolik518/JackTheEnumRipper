using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace JackTheEnumRipper.Core
{
    public static class Project
    {
        public static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        public static string BasePath { get; } = Directory.GetCurrentDirectory();

        public static string CompileTimeEnvironment
        {
            get
            {
#if DEBUG
                return Environments.Development;
#else
                return Environments.Production;
#endif
            }
        }

        public static string Name
        {
            get
            {
                return Assembly.GetName().Name ?? string.Empty;
            }
        }

        public static string Description
        {
            get
            {
                return Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? string.Empty;
            }
        }

        public static string Version
        {
            get
            {
                return Assembly.GetName().Version?.ToString() ?? string.Empty;
            }
        }
    }
}
