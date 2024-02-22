static class WriterFactory
{
    public static IEnumWriter GetWriter(string format, string outputDir)
    {
        switch (format.ToLower())
        {
            case "json":
                return new JsonWriter(outputDir);
            case "ini":
                return new IniWriter(outputDir);
            case "rust":
                return new RustWriter(outputDir);
            case "php":
                 return new PhpWriter(outputDir);
            case "cs":
            default:
                return new CSharpWriter(outputDir);
        }
    }
}