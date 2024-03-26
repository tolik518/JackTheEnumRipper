using System;
using System.Collections.Generic;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class RustSerializer : ISerializer
    {
        public Format Format => Format.Rust;

        public void Serialize(IEnumerable<AbstractEnum> enums, string path)
        {
            throw new NotImplementedException();
        }
    }
}