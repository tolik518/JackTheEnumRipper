using System;
using System.IO;

class IniWriter : IEnumWriter
{
    private readonly string _outputDir;

    public IniWriter(string outputDir)
    {
        _outputDir = outputDir;
    }

    public void WriteEnum(Type enumType, string fileName)
    {
        var filePath = Path.Combine(_outputDir, $"{fileName}.ini");

        using (StreamWriter file = new StreamWriter(filePath))
        {
            // Write the section header for the enum
            file.WriteLine($"[{enumType.Name}]");

            var values = Enum.GetValues(enumType);
            for (int i = 0; i < values.Length; i++)
            {
                var value = values.GetValue(i);
                var convertedValue = Convert.ChangeType(value, Enum.GetUnderlyingType(enumType));
                WriteValue(file, value.ToString(), convertedValue);
            }
        }
    }

    private void WriteValue(StreamWriter file, string name, object value)
    {
        file.WriteLine($"{name} = {value}");
    }
}
