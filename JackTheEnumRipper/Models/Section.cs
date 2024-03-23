using System.Collections.Generic;

namespace JackTheEnumRipper.Models
{
    public readonly record struct Section
    {
        public string Comment { get; init; }

        public required string Name { get; init; }

        public required IEnumerable<Setting> Settings { get; init; }

        public bool IsGlobal { get; init; }
    }
}
