using System;
using System.IO;

class JsonWriter : IEnumWriter
{
    private readonly string _outputDir;

    public JsonWriter(string outputDir)
    {
        _outputDir = outputDir;
    }

    public void WriteEnum(Type enumType, string fileName)
    {
        var filePath = Path.Combine(_outputDir, $"{fileName}.json");

        using (StreamWriter file = new StreamWriter(filePath))
        {
            // Initialize the JSON structure
            file.WriteLine("{");
            file.WriteLine($"  \"{enumType.Name}\": {{");

            var values = Enum.GetValues(enumType);
            for (int i = 0; i < values.Length; i++)
            {
                var value = values.GetValue(i);
                var convertedValue = Convert.ChangeType(value, Enum.GetUnderlyingType(enumType));
                // Determine if this is the last value to decide whether to append a comma
                bool isLast = i == values.Length - 1;
                WriteValue(file, value.ToString(), convertedValue, isLast);
            }

            // Finalize the JSON structure
            file.WriteLine("  }");
            file.WriteLine("}");
        }
    }

    private void WriteValue(StreamWriter file, string name, object value, bool isLast)
    {
        string comma = isLast ? "" : ",";
        file.WriteLine($"    \"{name}\": {value}{comma}");
    }
}
