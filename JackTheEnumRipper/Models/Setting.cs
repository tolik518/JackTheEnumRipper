namespace JackTheEnumRipper.Models
{
    public readonly record struct Setting
    {
        public string Comment { get; init; }

        public required string Name { get; init; }

        public required object Value { get; init; }
    }
}
