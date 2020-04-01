using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    internal class Report
    {
        public const string REPORT_TYPE_CSP = "csp";
        public const string REPORT_TYPE_INTERVENTION = "intervention";
        public const string REPORT_TYPE_DEPRECATION = "deprecation";
        public const string REPORT_TYPE_NETWORK_ERROR = "network-error";

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("age")]
        public uint Age { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("user_agent")]
        public string UserAgent { get; set; }

        [JsonPropertyName("body")]
        public IDictionary<string, object> Body { get; set; }
    }
}
