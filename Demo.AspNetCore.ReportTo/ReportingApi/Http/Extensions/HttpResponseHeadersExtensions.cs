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
                    response.Headers[HeaderNames.ReportTo] = (StringValues)response.Headers[HeaderNames.ReportTo].Append(reportToHeaderValue);
                }
                else
                {
                    response.Headers[HeaderNames.ReportTo] = reportToHeaderValue;
                }
            }
        }
    }
}
