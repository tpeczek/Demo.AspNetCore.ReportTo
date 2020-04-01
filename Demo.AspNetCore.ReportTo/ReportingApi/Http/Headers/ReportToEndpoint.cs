using System;
using System.Text.Json.Serialization;

namespace Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers
{
    internal struct ReportToEndpoint
    {
        [JsonPropertyName("url")]
        public string Url { get; }

        [JsonPropertyName("priority")]
        public uint? Priority { get; }

        [JsonPropertyName("weight")]
        public uint? Weight { get; }

        public ReportToEndpoint(string url, uint? priority = null, uint? weight = null)
            : this()
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Priority = priority;
            Weight = weight;
        }
    }
}
