using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Demo.AspNetCore.ReportTo.ReportingApi;
using Demo.AspNetCore.ReportTo.ReportingApi.Http.Headers;
using Demo.AspNetCore.ReportTo.ReportingApi.Http.Extensions;
using Microsoft.Extensions.Hosting;

namespace Demo.AspNetCore.ReportTo
{
    public class Startup
    {
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
                    ReportToHeaderValue reportToValue = new ReportToHeaderValue();
                    reportToValue.Endpoints.Add(new ReportToEndpoint("https://localhost:5001/report-to-endpoint"));

                    ctx.Context.Response.AddReportToResponseHeader(reportToValue);
                }
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReportToEndpoint("/report-to-endpoint");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("-- Demo.AspNetCore.ReportTo --");
            });
        }
    }
}
