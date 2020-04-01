using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace Demo.AspNetCore.ReportTo.ReportingApi
{
    internal static class EndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapReportToEndpoint(this IEndpointRouteBuilder endpoints, string pattern)
        {
            if (endpoints == null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            RequestDelegate pipeline = endpoints.CreateApplicationBuilder()
               .UseMiddleware<ReportToEndpointMiddleware>()
               .Build();

            return endpoints.Map(pattern, pipeline);
        }
    }
}
