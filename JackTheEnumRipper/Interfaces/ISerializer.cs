using System.Collections.Generic;

using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializer
    {
        public Format Format { get; }

        public void Serialize(IEnumerable<AbstractEnum> enums, string path);
    }
}