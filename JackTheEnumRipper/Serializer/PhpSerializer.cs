using System;

using JackTheEnumRipper.Interfaces;

namespace Serializer
{
    public class PhpSerializer : ISerializer
    {
        public string Name => "php";

        public void Serialize()
        {
            Console.WriteLine(this.Name);
        }
    }
}