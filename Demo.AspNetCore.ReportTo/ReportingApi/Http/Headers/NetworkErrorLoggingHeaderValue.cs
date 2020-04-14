using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers
{
    internal class NetworkErrorLoggingHeaderValue
    {
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { IgnoreNullValues = true };

        private decimal? _successFration;
        private decimal? _failureFration;

        [JsonPropertyName("report_to")]
        public string ReportTo { get; }

        [JsonPropertyName("max_age")]
        public uint MaxAge { get; }

        [JsonPropertyName("include_subdomains")]
        public bool? IncludeSubdomains { get; set; }

        [JsonPropertyName("success_fraction")]
        public decimal? SuccessFraction
        {
            get { return _successFration; }

            set
            {
                if ((value < 0) || (value > 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(SuccessFraction));
                }

                _successFration = value;
            }
        }

        [JsonPropertyName("failure_fraction")]
        public decimal? FailureFraction
        {
            get { return _failureFration; }

            set
            {
                if ((value < 0) || (value > 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(FailureFraction));
                }

                _failureFration = value;
            }
        }

        public NetworkErrorLoggingHeaderValue(string reportTo, uint maxAge)
        {
            ReportTo = reportTo ?? throw new ArgumentNullException(nameof(reportTo));
            MaxAge = maxAge;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, _serializerOptions);
        }
    }
}
