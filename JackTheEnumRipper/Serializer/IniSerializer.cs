using System;
using System.Collections.Generic;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class IniSerializer : ISerializer
    {
        public Format Format => Format.Ini;

        public void Serialize(IEnumerable<AbstractEnum> enums, string path)
        {
            Console.WriteLine(this.Format);
        }
    }
}