using System.Collections.Generic;

using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializerFactory
    {
        public IEnumerable<ISerializer> Serializers { get; }

        public ISerializer? Create(Format format);
    }
}
