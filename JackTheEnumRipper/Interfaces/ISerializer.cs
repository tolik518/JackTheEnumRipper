using JackTheEnumRipper.Models;

namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializer
    {
        public Format Format { get; }

        public void Serialize(string path);
    }
}