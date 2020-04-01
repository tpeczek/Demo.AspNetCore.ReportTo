using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers
{
    internal class ReportToHeaderValue
    {
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { IgnoreNullValues = true };

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("include_subdomains")]
        public bool? IncludeSubdomains { get; set; }

        [JsonPropertyName("max_age")]
        public uint MaxAge { get; set; } = 10886400;

        [JsonPropertyName("endpoints")]
        public IList<ReportToEndpoint> Endpoints { get; } = new List<ReportToEndpoint>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, _serializerOptions);
        }
    }
}
