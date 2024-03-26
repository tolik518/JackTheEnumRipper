using System.Collections.Generic;
using System.IO;
using System.Text;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

namespace Serializer
{
    public class JsonSerializer(IOptions<AppSettings> appSettings) : ISerializer
    {
        public Format Format => Format.Json;

        private readonly AppSettings _appSettings = appSettings.Value;

        public void Serialize(IEnumerable<AbstractEnum> enums, string path)
        {
            var encoding = Encoding.GetEncoding(this._appSettings.Encoding);
            string json = JsonConvert.SerializeObject(enums, Formatting.Indented);
            File.WriteAllText(path, json, encoding);
        }
    }
}