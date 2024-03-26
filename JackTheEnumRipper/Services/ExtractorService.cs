using System.Collections.Generic;
using System.IO;
using System.Linq;

using JackTheEnumRipper.Interfaces;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Mono.Cecil;

namespace JackTheEnumRipper.Services
{
    public class ExtractorService(ILogger<ExtractorService> logger, IHostEnvironment environment) : IExtractorService
    {
        private readonly ILogger<ExtractorService> _logger = logger;
        private readonly IHostEnvironment _environment = environment;

        public IEnumerable<TypeDefinition> ExtractEnums(string path)
        {
            string fullPath = Path.Join(this._environment.ContentRootPath, path);

            if (!Path.Exists(fullPath))
            {
                this._logger.LogError("Assembly Path: {Path}", fullPath);
                throw new DirectoryNotFoundException("The ExtractorService suspends its operation because it cannot locate the path to an assembly.");
            }

            var assemblyDefinition = AssemblyDefinition.ReadAssembly(fullPath);

            return assemblyDefinition.Modules
                .SelectMany(x => x.Types)
                .Where(x => x.IsEnum);
        }
    }
}
