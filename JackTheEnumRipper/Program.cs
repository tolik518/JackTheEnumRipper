using Mono.Cecil;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

using McMaster.Extensions.CommandLineUtils;
using System.Reflection;
using System.Collections.Generic;

namespace JackTheEnumRipper
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var app = new CommandLineApplication
            {
                Name = assembly.GetName().Name,
                Description = assembly.GetAttribute<AssemblyDescriptionAttribute>()?.Description,
            };

            app.HelpOption(inherited: true);
            var version = app.Option("-v|--version", "display program version and exit", CommandOptionType.NoValue);

            app.Command("format", exportCommand =>
            {
                exportCommand.Description = "provide one or more export format";
                var listOption = exportCommand.Option("--list", "list all available export formats and exit", CommandOptionType.NoValue);
                var formatArguments = exportCommand.Argument("writer", "the name of a format writer", multipleValues: true);

                exportCommand.OnExecute(() =>
                {
                    if (listOption.HasValue())
                    {
                        IEnumerable<string> availableFormats = Utils.GetAvailableFormats();
                        Console.WriteLine(string.Join(",", availableFormats));
                        Environment.Exit(0);
                    }


                    IReadOnlyList<string?> values = formatArguments.Values.Any() ? formatArguments.Values : [ "csharp" ];

                    foreach (var arg in formatArguments.Values)
                    {
                        Console.WriteLine(arg);
                    }
                });
            });

            app.OnExecute(() =>
            {
                if (version.HasValue())
                {
                    Console.WriteLine($"{app.Name}, version {assembly.GetName().Version}");
                }
                else
                {
                    app.ShowHelp();
                }
            });

            return app.Execute(args);

            //string assemblyPath = args[0];
            //if (!File.Exists(assemblyPath))
            //{
            //    Console.WriteLine($"File not found: {assemblyPath}");
            //    return;
            //}

            //string formatArg = args.Length > 1 ? args[1] : "--csharp"; // Default to csharp if no exportCommand is provided
            //if (!formatArg.StartsWith("--"))
            //{
            //    Console.WriteLine("Invalid exportCommand. Use --exportCommand. Example: --csharp");
            //    return;
            //}

            //string exportCommand = formatArg.Substring(2).ToLower();
            //ReadAssemblyAndExtractEnums(exportCommand, assemblyPath);
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
                Console.WriteLine($"Output directory: \"{outputDir}\"");
                Console.WriteLine("Operation completed");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No access to given file or the file is not written using .Net");
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
    } 
}