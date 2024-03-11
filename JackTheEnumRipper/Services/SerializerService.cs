using System;
using System.Collections.Generic;
using System.Linq;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Services
{
    public class SerializerService(ISerializerFactory serializerFactory) : ISerializerService
    {
        private readonly ISerializerFactory _serializerFactory = serializerFactory;

        public void Export(Format format)
        {
            var serializer = this._serializerFactory.Create(format);
            serializer?.Serialize();
        }

        public IEnumerable<string> GetAvailableFormats()
        {
            return this._serializerFactory.Serializers.Select(x => Enum.GetName(x.Format)!);
        }
    }
}
