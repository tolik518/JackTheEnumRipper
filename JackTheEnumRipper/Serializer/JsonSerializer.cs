using System;

using JackTheEnumRipper.Interfaces;

namespace Serializer
{
    public class JsonSerializer : ISerializer
    {
        public string Name => "json";

        public void Serialize()
        {
            Console.WriteLine(this.Name);
        }
    }
}