using System;
using System.Collections.Generic;
using System.Linq;

using JackTheEnumRipper.Interfaces;

namespace JackTheEnumRipper.Services
{
    public class SerializerService : ISerializerService
    {
        private readonly ISerializerFactory _serializerFactory;

        public SerializerService(ISerializerFactory serializerFactory)
        {
            this._serializerFactory = serializerFactory;
        }

        public void Export(string format)
        {
            var serializer = this._serializerFactory.Create(format);

            ArgumentNullException.ThrowIfNull(serializer, nameof(serializer));

            serializer.Serialize();
        }

        public IEnumerable<string> GetAvailableFormats()
        {
            return this._serializerFactory.Serializers.Select(x => x.Name);
        }
    }
}
