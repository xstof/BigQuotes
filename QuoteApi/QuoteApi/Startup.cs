using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuoteApi.Services;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace QuoteApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Add framework services.
            services.AddMvc();

            // Add Swashbuckle for automated swagger generation.
            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1",
                               new Swashbuckle.AspNetCore.Swagger.Info()
                               {
                                   Title = "Quote API",
                                   Version = "1"
                               });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = "wwwroot\\QuoteApi.xml";
                // var xmlPath = Path.Combine(basePath, "wwwroot\\QuoteApi.xml");
                opt.IncludeXmlComments(xmlPath);

                // Add http and https schemes to the generated swagger doc:
                opt.DocumentFilter<SwaggerSchemeFilter>();
            });

            // Add services
            services.AddTransient<IQuotesService, InMemoryQuotesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(opt => {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Quote API");
            });
        }

        /// <summary>
        /// Swashbuckle filter which adds both http and https schemes to the generated open api definition
        /// </summary>
        public class SwaggerSchemeFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
        {
            private readonly IConfiguration config;

            public SwaggerSchemeFilter(IConfiguration config)
            {
                this.config = config;
            }

            public void Apply(Swashbuckle.AspNetCore.Swagger.SwaggerDocument swaggerDoc,
                              Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext context)
            {
                swaggerDoc.Schemes = new string[] { "http", "https" };

                // Determine host:
                var host = "localhost";
                if(! String.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME"))) {
                    host = System.Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
                }

                swaggerDoc.Host = host;
            }
        }

    }
}
