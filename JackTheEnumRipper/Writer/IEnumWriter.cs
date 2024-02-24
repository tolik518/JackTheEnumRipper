using Mono.Cecil;

interface IEnumWriter
{
    void WriteEnum(TypeDefinition enumType, string fileName);
}
