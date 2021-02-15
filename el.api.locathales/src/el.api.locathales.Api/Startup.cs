using el.api.locathales.Api.Filters;
using el.api.locathales.Api.Logging;
using el.api.locathales.CrossCutting.Assemblies;
using el.api.locathales.CrossCutting.IoC;
using el.api.locathales.Domain;
using el.api.locathales.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;

namespace el.api.locathales.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

            services.AddLoggingSerilog();

            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.AddDependencyResolver();

           services.AddDbContext<LocaThalesContext>(x => x.UseSqlite("Data Source=data.db"));


            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "el.api.locathales",
                    Description = "API - el.api.locathales",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "el.api.locathales.Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "el.api.locathales.Application.xml");

                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(applicationPath);
            });

            services.AddHttpClient(Parametros.Fipe.Nome, configuracao => { configuracao.BaseAddress = Parametros.Fipe.UriFipe; configuracao.DefaultRequestVersion = HttpVersion.Version20; });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LocaThalesContext>();
                //context.Database.Migrate();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/el.api.locathales");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/el.api.locathales/swagger/v1/swagger.json", "API el.api.locathales");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
