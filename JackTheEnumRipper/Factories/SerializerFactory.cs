using System;
using System.Collections.Generic;
using System.Linq;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Factories
{
    public class SerializerFactory : ISerializerFactory
    {
        public IEnumerable<ISerializer> Serializers { get; }

        public SerializerFactory(Func<IEnumerable<ISerializer>> factory)
        {
            this.Serializers = factory();
        }

        public ISerializer? Create(Format format)
        {
            return this.Serializers.FirstOrDefault(x => x.Format == format);
        }
    }
}
