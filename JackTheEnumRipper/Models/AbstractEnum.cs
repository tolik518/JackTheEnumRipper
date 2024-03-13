using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTheEnumRipper.Models
{
    public readonly record struct AbstractEnum
    {
        public required string Namespace { get; init; }

        public required string Scope { get; init; }

        public required string Name { get; init; }

        public required string Type { get; init; }

        public required IEnumerable<AbstractField> Fields { get; init; }
    }
}
