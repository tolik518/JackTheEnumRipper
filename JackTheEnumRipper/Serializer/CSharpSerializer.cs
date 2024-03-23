using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace Serializer
{
    public class CSharpSerializer : ISerializer
    {
        public Format Format => Format.CSharp;

        private static CodeCompileUnit GenerateEnumCode(IEnumerable<AbstractEnum> enums)
        {
            var compileUnit = new CodeCompileUnit();

            foreach (IGrouping<string, AbstractEnum> enumGroup in enums.GroupBy(x => x.Namespace))
            {
                using IEnumerator<AbstractEnum> enumerator = enumGroup.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    AbstractEnum abstractEnum = enumerator.Current;
                    var @namespace = new CodeNamespace(abstractEnum.Namespace);

                    var @enum = new CodeTypeDeclaration(abstractEnum.Name)
                    {
                        IsEnum = true
                    };

                    @enum.Attributes |= abstractEnum.IsPublic ? MemberAttributes.Public : MemberAttributes.Assembly;

                    foreach (AbstractField field in abstractEnum.Fields)
                    {
                        @enum.Members.Add(new CodeMemberField(abstractEnum.Type, field.Name)
                        {
                            InitExpression = new CodePrimitiveExpression(field.Value)
                        });
                    }

                    @namespace.Types.Add(@enum);
                    compileUnit.Namespaces.Add(@namespace);
                }
            }

            return compileUnit;
        }

        public void Serialize(IEnumerable<AbstractEnum> enums, string path)
        {
            var provider = CodeDomProvider.CreateProvider(Enum.GetName(this.Format));
            var codeCompileUnit = GenerateEnumCode(enums);
            var options = new CodeGeneratorOptions
            {
                BracingStyle = "C",
                BlankLinesBetweenMembers = false,
                IndentString = "    "
            };

            using StringWriter writer = new();
            provider.GenerateCodeFromCompileUnit(codeCompileUnit, writer, options);
            File.WriteAllText(path, writer.ToString());
        }
    }
}