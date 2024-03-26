using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

using Microsoft.Extensions.Options;

namespace JackTheEnumRipper.Serializer
{
    public class PythonSerializer(IOptions<AppSettings> appSettings) : ISerializer
    {
        public Format Format => Format.Python;

        private readonly AppSettings _appSettings = appSettings.Value;

        public void Serialize(IEnumerable<AbstractEnum> enums, string path)
        {
            var builder = new StringBuilder();

            // TODO

            var encoding = Encoding.GetEncoding(this._appSettings.Encoding);
            string content = builder.ToString();
            File.WriteAllText(path, content, encoding);
        }
    }
}
