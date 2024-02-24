using Mono.Cecil;
using System.Collections.Generic;
using System.IO;

class JsonWriter : IEnumWriter
{
    private readonly string _outputDir;

    public JsonWriter(string outputDir)
    {
        _outputDir = outputDir;
    }

    void IEnumWriter.WriteEnum(TypeDefinition enumType, IEnumerable<(string Name, object Value)> enumValues, string fileName)
    {
        var filePath = Path.Combine(_outputDir, $"{fileName}.json");
        var fileDirectory = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(fileDirectory);

        using (StreamWriter file = new StreamWriter(filePath))
        {
            file.WriteLine("{");
            file.WriteLine($"  \"{enumType.Name}\": {{");

            foreach (var enumField in enumValues)
            {
                WriteValue(file, enumField.Name, enumField.Value);
            }

            file.WriteLine("  }");
            file.WriteLine("}");
        }
    }

    private void WriteValue(StreamWriter file, string name, object value)
    {
        file.WriteLine($"    \"{name}\": {value},");
    }
}
