using System;
using System.Collections.Generic;
using System.Linq;

using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Services
{
    public class SerializerService(ISerializerFactory serializerFactory, IExtractorService extractorService) : ISerializerService
    {
        private readonly ISerializerFactory _serializerFactory = serializerFactory;
        private readonly IExtractorService _extractorService = extractorService;

        public void Serialize(Format format, string path)
        {
            var serializer = this._serializerFactory.Create(format);

            ArgumentNullException.ThrowIfNull(serializer, nameof(serializer));

            // TODO
            var enums = this._extractorService.ExtractEnums(path);

            serializer.Serialize(path);
        }

        public IEnumerable<string> GetAvailableFormats()
        {
            return this._serializerFactory.Serializers.Select(x => Enum.GetName(x.Format)?.ToLower()!);
        }
    }
}
