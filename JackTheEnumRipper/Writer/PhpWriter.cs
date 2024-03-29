﻿using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class PhpWriter : IEnumWriter
{
    private readonly string _outputDir;

    public PhpWriter(string outputDir)
    {
        _outputDir = outputDir;
    }

    void IEnumWriter.WriteEnum(TypeDefinition enumType, IEnumerable<(string Name, object Value)> enumValues, string fileName)
    {
        var filePath = Path.Combine(_outputDir, $"{fileName}.php");
        var fileDirectory = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(fileDirectory);

        using (StreamWriter file = new StreamWriter(filePath))
        {
            string phpEnumName = ConvertToPascalCase(enumType.Name);
            file.WriteLine("<?php");
            file.WriteLine($"// Generated by JackTheEnumRipper");
            file.WriteLine($"enum {phpEnumName} : int {{"); //TODO: We need to add the Type in the future here 

            foreach (var enumField in enumValues)
            {
                string fieldName = ConvertToPascalCase(enumField.Name);
                var fieldValue = Convert.ToInt32(enumField.Value); //TODO: It should be an always an int
                WriteValue(file, fieldName, fieldValue);
            }

            file.WriteLine("}");
        }
    }

    private void WriteValue(StreamWriter file, string name, int value)
    {
        file.WriteLine($"\tcase {name} = {value};");
    }

    private string ConvertToPascalCase(string input)
    {
        // Simple conversion to PascalCase for now; I should consider edge cases and improvements
        TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
        return string.Concat(input.Split(new char[] { '_', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(word => textInfo.ToTitleCase(word.ToLower())));
    }
}
