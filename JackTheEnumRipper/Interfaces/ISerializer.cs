namespace JackTheEnumRipper.Interfaces
{
    public interface ISerializer
    {
        public string Name { get; }

        public void Serialize();
    }
}