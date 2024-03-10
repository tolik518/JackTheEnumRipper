using System;

using JackTheEnumRipper.Interfaces;

namespace Serializer
{
    public class RustSerializer : ISerializer
    {
        public string Name => "rust";

        public void Serialize()
        {
            Console.WriteLine(this.Name);
        }
    }
}