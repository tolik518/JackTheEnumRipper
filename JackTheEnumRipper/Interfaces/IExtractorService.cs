using System.Collections.Generic;

using Mono.Cecil;

namespace JackTheEnumRipper.Interfaces
{
    public interface IExtractorService
    {
        public IEnumerable<TypeDefinition> ExtractEnums(string path);
    }
}