using System;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class IniSerializer : ISerializer
    {
        public Format Format => Format.Ini;

        public void Serialize()
        {
            Console.WriteLine(this.Format);
        }
    }
}