using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTheEnumRipper.Models
{
    public readonly record struct AbstractField
    {
        public required string Name { get; init; }

        public required object Value { get; init; }
    }
}
