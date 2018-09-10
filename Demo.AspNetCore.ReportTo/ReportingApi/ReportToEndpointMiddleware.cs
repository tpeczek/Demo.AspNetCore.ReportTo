using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    internal class ReportToEndpointMiddleware
    {
        private const string _reportContentType = "application/reports+json";

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ReportToEndpointMiddleware(RequestDelegate next, ILogger<ReportToEndpointMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsReportsRequest(context.Request))
            {
                List<Report> reports = null;

                using (StreamReader requestBodyReader = new StreamReader(context.Request.Body))
                {
                    using (JsonReader requestBodyJsonReader = new JsonTextReader(requestBodyReader))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        reports = serializer.Deserialize <List<Report>>(requestBodyJsonReader);
                    }
                }

                LogReports(reports);

                context.Response.StatusCode = StatusCodes.Status204NoContent;
            }
            else
            {
                await _next(context);
            }
        }

        private bool IsReportsRequest(HttpRequest request)
        {
            return HttpMethods.IsPost(request.Method) && (request.ContentType == _reportContentType);
        }

        private void LogReports(List<Report> reports)
        {
            if (reports != null)
            {
                foreach (Report report in reports)
                {
                    switch (report.Type.ToLowerInvariant())
                    {
                        case Report.REPORT_TYPE_DEPRECATION:
                            _logger.LogWarning("Deprecation reported for file {SourceFile} (Line: {LineNumber}, Column: {ColumnNumber}): '{Message}'",
                                report.Body["sourceFile"], report.Body["lineNumber"], report.Body["columnNumber"], report.Body["message"]);
                            break;
                        default:
                            _logger.LogInformation("Browser report of type '{ReportType}' received.", report.Type);
                            break;
                    }
                }
            }
        }
    }
}
