using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using JackTheEnumRipper.Core;
using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class CSharpSerializer : ISerializer
    {
        public Format Format => Format.CSharp;

        public void Serialize(IEnumerable<AbstractEnum> enums, string path)
        {
            StringBuilder builder = new();
            string tab = " ".Repeat(4);

            foreach (IGrouping<string, AbstractEnum> enumGroup in enums.GroupBy(x => x.Namespace))
            {
                builder.AppendLine($"namespace {enumGroup.Key}");
                builder.AppendLine("{");

                using IEnumerator<AbstractEnum> enumerator = enumGroup.GetEnumerator();
                bool last = !enumerator.MoveNext();

                while (!last)
                {
                    AbstractEnum @enum = enumerator.Current;
                    builder.AppendLine($"{tab}{@enum.Scope} enum {@enum.Name} : {@enum.Type}");
                    builder.AppendLine($"{tab}{{");

                    foreach (AbstractField field in @enum.Fields)
                    {
                        builder.AppendLine($"{tab}{tab}{field.Name} = {field.Value},");
                    }

                    builder.AppendLine($"{tab}}}");
                    last = !enumerator.MoveNext();
                    if (!last) builder.AppendLine();
                }

                builder.AppendLine("}");
            }

            string code = builder.ToString();
            File.WriteAllText(path, code, encoding: Encoding.UTF8);
        }
    }
}