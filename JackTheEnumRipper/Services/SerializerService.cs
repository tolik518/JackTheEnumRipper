using System;
using System.Collections.Generic;
using System.Linq;

using JackTheEnumRipper.Core;
using JackTheEnumRipper.Interfaces;
using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Services
{
    public class SerializerService(ISerializerFactory serializerFactory, IExtractorService extractorService) : ISerializerService
    {
        private readonly ISerializerFactory _serializerFactory = serializerFactory;
        private readonly IExtractorService _extractorService = extractorService;

        public void Serialize(Format format, string assemblyPath, string filePath)
        {
            var serializer = this._serializerFactory.Create(format);

            ArgumentNullException.ThrowIfNull(serializer, nameof(serializer));

            var types = this._extractorService.ExtractEnums(assemblyPath);
            var enums = Utils.ParseEnum(types);
            serializer.Serialize(enums, filePath);
        }

        public IEnumerable<string> GetAvailableFormats()
        {
            return this._serializerFactory.Serializers.Select(x => Enum.GetName(x.Format)?.ToLower()!);
        }
    }
}
