using System.IO;
using System.Linq;
using System.Reflection;
using System;
using System.Globalization;
using System.Collections.Generic;

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
            Console.WriteLine(GetAvailableWriters(prefix: false)); 
            return;
        }

        Console.Title = $"JackTheEnumRipper v{version}";
        PrintBanner();

        if (args.Length == 0 || args.Contains("--help") || args.Contains("-h"))
        {
            Console.WriteLine("Usage: JackTheEnumRipper <assembly> <format>");
            Console.WriteLine("  <format>: The output format. Supported formats: " + GetAvailableWriters(prefix: true));
            Console.ReadLine();
            return;
        }

        string assemblyPath = args[0];
        if (!File.Exists(assemblyPath))
        {
            Console.WriteLine($"File not found: {assemblyPath}");
            Console.ReadLine();
            return;
        }

        try
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var assemblyName = assembly.GetName().Name;
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var outputDir = Path.Combine(path, $"Enums.{assemblyName}");
            Directory.CreateDirectory(outputDir); // Ensure the directory exists

            // get the format from the command line and remove the -- prefix
            string format = args.Skip(1).FirstOrDefault()?.TrimStart('-') ?? "csharp";

            IEnumWriter writer = GetWriterForFormat(format, outputDir);
            var ripper = new EnumRipper(writer);
            ripper.ExtractEnumsFromAssembly(assemblyPath);
            Console.WriteLine($"Operation completed");
            Console.ReadLine();
        }
        catch (BadImageFormatException ex)
        {
            Console.WriteLine("The assembly cannot be loaded, likely due to a bitness (32bit vs 64bit) mismatch or it's not a .NET assembly.");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("The specified assembly was not found.");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading assembly.");
            Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            Console.ReadLine();
        }
    }

    private static IEnumWriter GetWriterForFormat(string format, string outputDir)
    {
        var writerTypeName = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(format)}Writer";
        var writerType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(t => typeof(IEnumWriter).IsAssignableFrom(t) && !t.IsInterface && t.Name.Equals(writerTypeName, StringComparison.OrdinalIgnoreCase));

        if (writerType != null)
        {
            return (IEnumWriter) Activator.CreateInstance(writerType, new object[] { outputDir });
        }

        return null;
    }

    private static string GetAvailableWriters(bool prefix)
    {
        var writers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IEnumWriter).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => t.Name.Replace("Writer", "").ToLower());

        if (prefix)
        {
            return writers.Select(f => $"--{f}").Aggregate((a, b) => $"{a}, {b}");
        }

        return writers.Aggregate((a, b) => $"{a}, {b}");
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
