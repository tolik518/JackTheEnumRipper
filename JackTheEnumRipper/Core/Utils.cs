﻿using System;
using System.Collections.Generic;
using System.Linq;

using JackTheEnumRipper.Models;

using Mono.Cecil;

namespace JackTheEnumRipper.Core
{
    public class Utils
    {
        public static IEnumerable<AbstractEnum> ParseEnum(IEnumerable<TypeDefinition> types)
        {
            return types.Select(x => new AbstractEnum
            {
                Namespace = x.Namespace,
                IsPublic = x.IsPublic,
                Name = x.Name,
                Type = x.Fields.Select(y => y.FieldType.Name).First(),
                Fields = x.Fields.Skip(1).Select(y => new AbstractField { Name = y.Name, Value = y.Constant })
            });
        }

        public static string GetExtension(Format format)
        {
            return format switch
            {
                Format.CSharp => ".cs",
                Format.Ini => ".ini",
                Format.Json => ".json",
                Format.Php => ".php",
                Format.Rust => ".rs",
                Format.Python => ".py",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
