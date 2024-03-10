using System.Collections.Generic;

namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializerFactory
    {
        public IEnumerable<ISerializer> Serializers { get; }

        public ISerializer? Create(string format);
    }
}
