using System;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class CSharpSerializer : ISerializer
    {
        public Format Format => Format.CSharp;

        public void Serialize()
        {
            Console.WriteLine(this.Format);
        }
    }
}