using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
                List<Report> reports = await JsonSerializer.DeserializeAsync<List<Report>>(context.Request.Body);

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
                            LogReport(new DeprecationReportBody(report.Body));
                            break;
                        case Report.REPORT_TYPE_CRASH:
                            LogReport(new CrashReportBody(report.Body));
                            break;
                        default:
                            _logger.LogInformation("Browser report of type '{ReportType}' received.", report.Type);
                            break;
                    }
                }
            }
        }

        private void LogReport(DeprecationReportBody reportBody)
        {
            _logger.LogWarning("Deprecation reported for file {SourceFile} (Line: {LineNumber}, Column: {ColumnNumber}): '{Message}'",
                reportBody.SourceFile, reportBody.LineNumber, reportBody.ColumnNumber, reportBody.Message);
        }

        private void LogReport(CrashReportBody reportBody)
        {
            _logger.LogWarning("The website stopped running due to a browser crash. Reason: {Reason}", reportBody.Reason);
        }
    }
}
