using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers
{
    internal class ReportToHeaderValue
    {
        [JsonProperty(PropertyName = "group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }

        [JsonProperty(PropertyName = "include_subdomains", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IncludeSubdomains { get; set; }

        [JsonProperty(PropertyName = "max_age")]
        public uint MaxAge { get; set; } = 10886400;

        [JsonProperty(PropertyName = "endpoints")]
        public IList<ReportToEndpoint> Endpoints { get; } = new List<ReportToEndpoint>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
