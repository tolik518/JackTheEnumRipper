using System;
using System.IO;
using System.Linq;
using System.Reflection;

class EnumRipper
{
    private readonly IEnumWriter _writer;

    public EnumRipper(IEnumWriter writer)
    {
        _writer = writer;
    }

    public void ExtractEnumsFromAssembly(string outputDir, string assemblyPath)
    {
        try
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Console.WriteLine($"Successfully loaded {assembly.FullName}");

            foreach (Type type in assembly.GetTypes().Where(t => t.IsEnum))
            {
                // replace the dots in the type.Name with solder separator to create a folder structure
                var typeFolder = type.Name.Replace(".", Path.DirectorySeparatorChar.ToString());

                string fileName = Path.Combine(type.Namespace, typeFolder);
                    
                _writer.WriteEnum(type, fileName);
                Console.WriteLine($"Enum: {fileName}");
            }
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
}
