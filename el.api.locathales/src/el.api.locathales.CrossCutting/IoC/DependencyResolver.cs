using Microsoft.Extensions.DependencyInjection;
using el.api.locathales.Application;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;
using el.api.locathales.CrossCutting.Assemblies;
using System.Runtime.Caching;
using el.api.locathales.Domain;

namespace el.api.locathales.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            services.AddSingleton(new MemoryCache(Parametros.Cache.Prefixo));
            services.AddScoped<ICacheMemoriaRepository, CacheMemoriaRepository>();

            RegisterApplications(services);
            RegisterRepositories(services);
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            var applicationInterfaces = AssemblyUtil.GetApplicationInterfaces();
            var applicationClasses = AssemblyUtil.GetApplicationClasses();

            foreach (var @interface in applicationInterfaces)
            {
                var type = AssemblyUtil.FindType(@interface, applicationClasses);

                if (type != null)
                    services.AddScoped(@interface, type);
            }
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            var domainInterfaces = AssemblyUtil.GetRepositoryInterfaces();
            var repositories = AssemblyUtil.GetRepositories();

            foreach (var repo in repositories)
            {
                var @interface = AssemblyUtil.FindInterface(repo, domainInterfaces);

                if (@interface != null)
                    services.AddScoped(@interface, repo);
            }
        }
    }
}