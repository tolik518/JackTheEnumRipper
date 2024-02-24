using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    private static string version = "1.0.0";
    static void Main(string[] args)
    {
        // print the version and exit - could be useful when using with other tools
        if (args.Length == 1 && (args[0] == "--version" || args[0] == "-v"))
        {
            Console.WriteLine(version);
            return;
        }

        // print the supported formats and exit, also useful when using with other tools
        if (args.Length == 1 && (args[0] == "--formats" || args[0] == "-f"))
        {
            Console.WriteLine(GetAvailableWritersAsString(prefix: false));
            return;
        }

        Console.Title = $"JackTheEnumRipper v{version}";
        PrintBanner();

        if (args.Length == 0 || args.Contains("--help") || args.Contains("-h"))
        {
            Console.WriteLine("Usage: JackTheEnumRipper <assembly> <format>");
            Console.WriteLine("  <format>: The output format. Supported formats: " + GetAvailableWritersAsString(prefix: true));
            Console.ReadLine();
            return;
        }

        string assemblyPath = args[0];
        if (!File.Exists(assemblyPath))
        {
            Console.WriteLine($"File not found: {assemblyPath}");
            return;
        }

        string formatArg = args.Length > 1 ? args[1] : "--csharp"; // Default to csharp if no format is provided
        if (!formatArg.StartsWith("--"))
        {
            Console.WriteLine("Invalid format. Use --format. Example: --csharp");
            return;
        }

        string format = formatArg.Substring(2).ToLower();
        ReadAssemblyAndExtractEnums(format, assemblyPath);
    }

    private static void ReadAssemblyAndExtractEnums(string format, string assemblyPath)
    {
        try
        {
            var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
            var outputDir = Path.Combine(
                Path.GetDirectoryName(assemblyPath),
                $"Enums.{assembly.Name.Name}"
            );
            Directory.CreateDirectory(outputDir);

            var writer = GetWriterForFormat(format, outputDir);
            if (writer == null)
            {
                Console.WriteLine($"No writer found for format: {format}");
                Console.ReadLine();
                return;
            }

            var ripper = new EnumRipper(writer);
            ripper.ExtractEnumsFromAssembly(outputDir, assemblyPath);
            Console.WriteLine("Operation completed");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            Console.ReadLine();
        }
    }

    private static IEnumWriter GetWriterForFormat(string format, string outputDir)
    {
        var writerTypeName = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(format)}Writer";
        var writerType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => typeof(IEnumWriter).IsAssignableFrom(t) && !t.IsInterface && t.Name.Equals(writerTypeName, StringComparison.OrdinalIgnoreCase));

        if (writerType == null)
        {
            return null;
        }

        return (IEnumWriter)Activator.CreateInstance(writerType, new object[] { outputDir });
    }

    private static IEnumerable<string> GetAvailableWriters()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IEnumWriter).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => t.Name.Replace("Writer", "").ToLower());
    }

    private static string GetAvailableWritersAsString(bool prefix)
    {
        if (prefix)

        {
            return string.Join(", ", GetAvailableWriters().Select(f => $"--{f}"));
        }
        return string.Join(", ", GetAvailableWriters());
    }


    private static void PrintBanner()
    {
        Console.WriteLine("                                                                                ");
        Console.WriteLine("   ▄██▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██▄  ");
        Console.WriteLine("   █▀                                                                       ▀█  ");
        Console.WriteLine("   █      ▀███   ▄       ▄▄   ██   ▄    T   ▄██▀▀▀▀ █▄   ██ ██ ▐█ ██▄   ▄██  █  ");
        Console.WriteLine("   █       ██▌ ▄█▀█▄  ▄███▀█▄ ██▄██▀    H   ███▄▄▄  ███▄ ██ ██ ▐█ ████▄████  █  ");
        Console.WriteLine("   █   ▄▄  ██▌▄█████▄ ███▄    ██▀█▄     E   ███▀▀   ██▌▀███ ██▄▐█ ███ ██ ██  █  ");
        Console.WriteLine("   █  ███▄███▌█▀  ▀█▀  ▀████▀ ▀█ ▀██        ▀██████ ██▌  ▀█ ▀███▀ ██▀    ██  █  ");
        Console.WriteLine("   █   ▀▀▀▀▀▀      ▄▄▄▄▄▄      ▄▄▄▄▄▄   ▄▄▄▄▄▄   ▄▄▄▄▄▄▄  ▄▄▄▄▄▄             █  ");
        Console.WriteLine("   █              ███▀▀███ ▄▄ ███▀▀███ ███▀▀███ ████▀▀▀  ███▀▀███            █  ");
        Console.WriteLine("   █              ███ ▄█▀   ▌ ███▄▄██▀ ███▄▄██▀ ██████▀  ███ ▄█▀             █  ");
        Console.WriteLine("   █              ███▀▀██▄ ██ ███▀▀    ███▀▀    ███▌     ███▀▀██▄            █  ");
        Console.WriteLine("   █        ▄▄     ▀█  █▀▀▄█▀▄███▄    ▄███▄    ▄████████▄▀██▌  █▀   ▄▄       █  ");
        Console.WriteLine("   █       ████▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄████      █  ");
        Console.WriteLine("   ██▄      ▀▀                                                      ▀▀     ▄██  ");
        Console.WriteLine("    ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀   ");
        Console.WriteLine("                                                                                ");
    }
}