using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Demo.AspNetCore.ReportTo.ReportingApi;
using Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers;
using Demo.AspNetCore.ReportTo.ReportingApi.Http.Extensions;

namespace Demo.AspNetCore.ReportTo
{
    public class Startup
    {
        private const int NetworkErrorLoggingMaxAge = 24 * 60 * 60;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                DefaultFileNames = new List<string> { "reporting-api.html" }
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const string networkErrorLoggingGroupName = "network-errors";

                    ReportToHeaderValue defaultReportTo = new ReportToHeaderValue();
                    defaultReportTo.Endpoints.Add(new ReportToEndpoint("https://localhost:5001/report-to-endpoint"));

                    ReportToHeaderValue networkErrorLoggingReportTo = new ReportToHeaderValue { Group = networkErrorLoggingGroupName };
                    networkErrorLoggingReportTo.Endpoints.Add(new ReportToEndpoint("https://localhost:5001/report-to-endpoint"));

                    NetworkErrorLoggingHeaderValue networkErrorLoggingValue = new NetworkErrorLoggingHeaderValue(networkErrorLoggingGroupName, NetworkErrorLoggingMaxAge);
                    networkErrorLoggingValue.RequestHeaders.Add("accept-language");
                    networkErrorLoggingValue.RequestHeaders.Add("accept-encoding");

                    ctx.Context.Response.AddReportToResponseHeader(defaultReportTo);
                    ctx.Context.Response.AddReportToResponseHeader(networkErrorLoggingReportTo);
                    ctx.Context.Response.AddNetworkErrorLoggingResponseHeader(networkErrorLoggingValue);
                }
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReportToEndpoint("/report-to-endpoint");

                endpoints.Map("/xhr", context =>
                {
                    context.Response.StatusCode = 500;

                    return Task.CompletedTask;
                });
            });
        }
    }
}
