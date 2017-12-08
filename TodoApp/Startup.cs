using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

using TodoApp.Models;

namespace TodoApp
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// App configuration
        /// </summary>
        /// <returns></returns>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                // Define some basic meta data for the swagger output
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Todo Api",
                    Version = "v1",
                    Description = "A simple example project for ASP.NET Core Web Apis"
                });

                // Requires that you add this to your TodoApp.csproj:
                // <DocumentationFile>bin\$(Configuration)\$(TargetFramework)\$(AssemblyName).xml</DocumentationFile>
                // It will include all xml style comments in the swagger output documentation
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TodoApp.xml"));
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Setup swagger for development only, override the default route to merge it with the api routes,
                // "documentName" will be inserted by swagger and is required, in practive it will be the api version tag
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/docs/{documentName}/docs.json";
                });

                // Setup swagger ui on the domain/api-docs route
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
