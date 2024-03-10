using System.Collections.Generic;

namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializerService
    {
        public void Export(string format);

        public IEnumerable<string> GetAvailableFormats();
    }
}
