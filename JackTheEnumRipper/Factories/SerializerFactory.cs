using System;
using System.Collections.Generic;
using System.Linq;

using JackTheEnumRipper.Interfaces;

namespace JackTheEnumRipper.Factories
{
    public class SerializerFactory : ISerializerFactory
    {
        private readonly Func<IEnumerable<ISerializer>> _factory;

        public IEnumerable<ISerializer> Serializers { get; }

        public SerializerFactory(Func<IEnumerable<ISerializer>> factory)
        {
            this._factory = factory;
            this.Serializers = this._factory();
        }

        public ISerializer? Create(string format)
        {
            return this.Serializers.FirstOrDefault(x => string.Equals(x.Name, format, StringComparison.OrdinalIgnoreCase));
        }
    }
}
