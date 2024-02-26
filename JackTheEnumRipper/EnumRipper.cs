using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
            Console.WriteLine($"Successfully loaded {assembly.FullName}");

            foreach (var module in assembly.Modules)
            {
                foreach (TypeDefinition type in module.Types)
                {
                    ProcessType(type, outputDir, null);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            Console.ReadLine();
        }
    }

    private void ProcessType(TypeDefinition type, string outputDir, string parentNamespace)
    {
        string typeNamespace = GetTypeNamespace(type, parentNamespace);

        if (type.IsEnum)
        {
            WriteEnumToFile(type, outputDir, typeNamespace);
        }

        // Recursively process nested types
        foreach (var nestedType in type.NestedTypes)
        {
            ProcessType(nestedType, outputDir, typeNamespace);
        }
    }

    private string GetTypeNamespace(TypeDefinition type, string parentNamespace)
    {
        string namespacePath;
        if (type.IsNested) // Build the namespace from the parent if the type is nested, otherwise use the type's namespace
        {
            namespacePath = $"{parentNamespace}.{type.DeclaringType.Name}.{type.Name}";
        }
        else
        {
            namespacePath = (!string.IsNullOrEmpty(type.Namespace) ? type.Namespace : parentNamespace);
        }

        return namespacePath?.Replace(".", Path.DirectorySeparatorChar.ToString());
    }

    private void WriteEnumToFile(TypeDefinition enumType, string outputDir, string typeNamespace)
    {
        // Construct the full path using the namespace (and possibly nested class names)
        var folderPath = Path.Combine(outputDir, typeNamespace);
        Directory.CreateDirectory(folderPath); 

        var fileName = $"{enumType.Name}"; 
        var fullPath = Path.Combine(folderPath, fileName);
        var enumValues = GetEnumValues(enumType);

        _writer.WriteEnum(enumType, enumValues, fullPath);
        Console.WriteLine($"Enum: {typeNamespace}{Path.DirectorySeparatorChar}{fileName}");
    }

    private IEnumerable<(string Name, object Value)> GetEnumValues(TypeDefinition enumType)
    {
        // First, determine the underlying type of the enum
        var underlyingType = enumType.Fields.FirstOrDefault(f => f.Name.Equals("value__"))?.FieldType;
        if (underlyingType == null)
        {
            yield break; // not sure if this is possible, but just in case
        }

        var fields = enumType.Fields.Where(f => f.IsStatic && f.HasConstant);
        foreach (var field in fields)
        {
            yield return (field.Name, field.Constant);
        }
    }
}
