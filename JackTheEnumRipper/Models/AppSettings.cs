using System.Text;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JackTheEnumRipper.Models
{
    public record AppSettings
    {
        public required string Encoding { get; set; }
    }
}
