using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SelamRover.Common;
using SelamRover.Operation;
using SelamRover.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SelamRover.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(o => {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                o.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddScoped<IMoveService, MoveService>();
            services.AddScoped<IMoveOperation, MoveOperation>();

            services.AddTransient<MoveCommand>();
            services.AddTransient<TurnLeftCommand>();
            services.AddTransient<TurnRightCommand>();

            services.AddTransient<ServiceResolver>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "M":
                        return serviceProvider.GetService<MoveCommand>();
                    case "L":
                        return serviceProvider.GetService<TurnLeftCommand>();
                    case "R":
                        return serviceProvider.GetService<TurnRightCommand>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/healthcheck", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions {
                ResponseWriter = async (context, report) => {
                    var result = Newtonsoft.Json.JsonConvert.SerializeObject(
                            new { 
                                Failed = (report.Status != Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy), 
                                Message = report.Status.ToString(),
                            });
                    context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
        }
    }
}
