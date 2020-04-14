using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers;

namespace Demo.AspNetCore.ReportTo.ReportingApi.Http.Extensions
{
    internal static class HttpResponseHeadersExtensions
    {
        public static void AddReportToResponseHeader(this HttpResponse response, ReportToHeaderValue reportTo)
        {
            string reportToHeaderValue = reportTo?.ToString();

            if (!String.IsNullOrWhiteSpace(reportToHeaderValue))
            {
                if (response.Headers.ContainsKey(HeaderNames.ReportTo))
                {
                    response.Headers[HeaderNames.ReportTo] = new StringValues(response.Headers[HeaderNames.ReportTo].Append(reportToHeaderValue).ToArray());
                }
                else
                {
                    response.Headers[HeaderNames.ReportTo] = reportToHeaderValue;
                }
            }
        }

        public static void AddNetworkErrorLoggingResponseHeader(this HttpResponse response, NetworkErrorLoggingHeaderValue networkErrorLogging)
        {
            string networkErrorLoggingHeaderValue = networkErrorLogging?.ToString();

            if (!String.IsNullOrWhiteSpace(networkErrorLoggingHeaderValue))
            {
                if (response.Headers.ContainsKey(HeaderNames.NetworkErrorLogging))
                {
                    response.Headers[HeaderNames.NetworkErrorLogging] = new StringValues(response.Headers[HeaderNames.NetworkErrorLogging].Append(networkErrorLoggingHeaderValue).ToArray());
                }
                else
                {
                    response.Headers[HeaderNames.NetworkErrorLogging] = networkErrorLoggingHeaderValue;
                }
            }
        }
    }
}
