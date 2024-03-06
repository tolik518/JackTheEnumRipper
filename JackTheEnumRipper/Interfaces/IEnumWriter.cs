using Mono.Cecil;
using System.Collections.Generic;

interface IEnumWriter
{
    void WriteEnum(TypeDefinition enumType, IEnumerable<(string Name, object Value)> enumValues, string fileName);
}
