using System.Collections.Generic;
using Newtonsoft.Json;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    internal class Report
    {
        public const string REPORT_TYPE_CSP = "csp";
        public const string REPORT_TYPE_INTERVENTION = "intervention";
        public const string REPORT_TYPE_DEPRECATION = "deprecation";
        public const string REPORT_TYPE_NETWORK_ERROR = "network-error";

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "age")]
        public uint Age { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "user_agent")]
        public string UserAgent { get; set; }

        [JsonProperty(PropertyName = "body")]
        public IDictionary<string, object> Body { get; set; }
    }
}
