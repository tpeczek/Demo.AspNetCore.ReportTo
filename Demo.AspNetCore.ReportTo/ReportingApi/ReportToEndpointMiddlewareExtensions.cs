using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    internal static class ReportToEndpointMiddlewareExtensions
    {
        public static IApplicationBuilder MapReportToEndpoint(this IApplicationBuilder app, PathString pathMatch)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.Map(pathMatch, branchedApp => branchedApp.UseMiddleware<ReportToEndpointMiddleware>());
        }
    }
}
