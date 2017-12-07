using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace TodoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Todo Api",
                    Version = "v1",
                    Description = "A simple example project for ASP.NET Core Web Apis"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/docs/{documentName}/docs.json";
                });

                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "api-docs";
                    c.DocumentTitle("Todo API Docs");
                    c.SwaggerEndpoint("/api/docs/v1/docs.json", "Todo API v1");
                });
            }

            app.UseMvc();
        }
    }
}
