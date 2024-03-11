using System.Collections.Generic;

using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializerService
    {
        public void Export(Format format);

        public IEnumerable<string> GetAvailableFormats();
    }
}
