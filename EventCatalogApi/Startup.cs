using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventLite
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
            services.AddControllers();

 

            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabasePassword"];
            var connectionString = $"Server={server};Database={database};User Id={user};Password={password}";


            // Where? For creating CatalogContext (when the runtime does it)
            // The DbContextOptions to build needs to say that the where
            // is a sql server, specifically, this sql server that is specified
            // in the appsettings.json by "ConnectionString"

            //var connectionString = Configuration["ConnectionString"]);

            services.AddDbContext<CatalogContext>(builder =>
                builder.UseSqlServer(connectionString));


            // Added while walking through the cart api class
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "EventLite - Event Catalog API",
                    Version = "v1",
                    Description = "Event catalog microservice"
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();


            // Second change to Startup.cs for Swagger
            app.UseSwagger()
                .UseSwaggerUI(e =>
                {
                    e.SwaggerEndpoint("/swagger/v1/swagger.json", "EventCatalogAPI V1");
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
