using System.IO;
using System.Linq;
using System.Reflection;
using System;

class Program
{
    static void Main(string[] args)
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


        if (args.Length == 0)
        {
            Console.WriteLine("Usage: JackTheEnumRipper.exe <path_to_assembly> [--json]");
            return;
        }

        string assemblyPath = args[0];
        if (!File.Exists(assemblyPath))
        {
            Console.WriteLine($"File not found: {assemblyPath}");
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

            IEnumWriter writer = WriterFactory.GetWriter(format, outputDir);
            var ripper = new EnumRipper(writer);
            ripper.ExtractEnumsFromAssembly(assemblyPath);
        }
        catch (BadImageFormatException ex)
        {
            Console.WriteLine("The assembly cannot be loaded, likely due to a bitness (32bit vs 64bit) mismatch or it's not a .NET assembly.");
            Console.WriteLine(ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("The specified assembly was not found.");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading assembly.");
            Console.WriteLine($"{ex.GetType()}: {ex.Message}");
        }
    }
}
