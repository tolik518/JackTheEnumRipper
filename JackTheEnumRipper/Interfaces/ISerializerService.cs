using System.Collections.Generic;

using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializerService
    {
        public void Serialize(Format format, string path);

        public IEnumerable<string> GetAvailableFormats();
    }
}
