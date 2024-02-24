using Mono.Cecil;
using System.Collections.Generic;
using System.IO;

class IniWriter : IEnumWriter
{
    private readonly string _outputDir;

    public IniWriter(string outputDir)
    {
        _outputDir = outputDir;
    }

    void IEnumWriter.WriteEnum(TypeDefinition enumType, IEnumerable<(string Name, object Value)> enumValues, string fileName)
    {
        var filePath = Path.Combine(_outputDir, $"{fileName}.ini");
        var fileDirectory = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(fileDirectory);

        using (StreamWriter file = new StreamWriter(filePath))
        {
            // Write the section header for the enum
            file.WriteLine($"[{enumType.Name}]");

            // Iterate over fields to get enum values
            foreach (var enumField in enumValues)
            {
                WriteValue(file, enumField.Name, enumField.Value);
            }
        }
    }

    private void WriteValue(StreamWriter file, string name, object value)
    {
        file.WriteLine($"{name} = {value}");
    }
}
