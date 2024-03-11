﻿using System;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class RustSerializer : ISerializer
    {
        public Format Format => Format.Rust;

        public void Serialize()
        {
            Console.WriteLine(this.Format);
        }
    }
}