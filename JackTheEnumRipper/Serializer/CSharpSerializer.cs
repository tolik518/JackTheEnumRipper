using System;

using JackTheEnumRipper.Interfaces;

namespace Serializer
{
    public class CSharpSerializer : ISerializer
    {
        public string Name => "csharp";

        public void Serialize()
        {
            Console.WriteLine(this.Name);
        }
    }
}