using System;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class JsonSerializer : ISerializer
    {
        public Format Format => Format.Json;

        public void Serialize()
        {
            Console.WriteLine(this.Format);
        }
    }
}