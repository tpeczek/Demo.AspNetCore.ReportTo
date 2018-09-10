using Newtonsoft.Json;
using System;

namespace Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers
{
    internal struct ReportToEndpoint
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; }

        [JsonProperty(PropertyName = "priority", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Priority { get; }

        [JsonProperty(PropertyName = "weight", NullValueHandling = NullValueHandling.Ignore)]
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
