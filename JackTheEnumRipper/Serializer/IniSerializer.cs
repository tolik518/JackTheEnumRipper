using System;

using JackTheEnumRipper.Interfaces;

namespace Serializer
{
    public class IniSerializer : ISerializer
    {
        public string Name => "ini";

        public void Serialize()
        {
            Console.WriteLine(this.Name);
        }
    }
}