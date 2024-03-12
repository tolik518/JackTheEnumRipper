using System.Collections.Generic;
using System.IO;
using System.Text;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

using Newtonsoft.Json;

namespace Serializer
{
    public class JsonSerializer : ISerializer
    {
        public Format Format => Format.Json;

        public void Serialize(IEnumerable<AbstractEnum> enums, string path)
        {
            string json = JsonConvert.SerializeObject(enums, Formatting.Indented);
            File.WriteAllText(path, json, encoding: Encoding.UTF8);
        }
    }
}