using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace el.api.locathales.CrossCutting.Assemblies
{
    [ExcludeFromCodeCoverage]
    public class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {            
            return new Assembly[]
            {
                Assembly.Load("el.api.locathales.Api"),
                Assembly.Load("el.api.locathales.Application"),
                Assembly.Load("el.api.locathales.Domain"),
                Assembly.Load("el.api.locathales.Domain.Core"),
                Assembly.Load("el.api.locathales.Infrastructure"),
                Assembly.Load("el.api.locathales.CrossCutting")
            };
        }

        public static IEnumerable<Type> GetApplicationInterfaces()
        {
            return Assembly.Load("el.api.locathales.Application").GetTypes().Where(
                type => type.IsInterface
                && type.Namespace != null
                && type.Namespace.StartsWith("el.api.locathales.Application.Interfaces"));
        }

        public static IEnumerable<Type> GetApplicationClasses()
        {
            return Assembly.Load("el.api.locathales.Application").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Type> GetRepositoryInterfaces()
        {
            return Assembly.Load("el.api.locathales.Domain").GetTypes().Where(
                type => type.IsInterface
                && type.Namespace != null
                && type.Namespace.StartsWith("el.api.locathales.Domain.Repositories"));
        }

        public static IEnumerable<Type> GetRepositories()
        {
            return Assembly.Load("el.api.locathales.Infrastructure").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.Namespace != null
                && type.Namespace.StartsWith("el.api.locathales.Infrastructure.Repositories")
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static Type FindType(Type @interface, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return type;
                }
            }

            return null;
        }

        public static Type FindInterface(Type type, IEnumerable<Type> interfaces)
        {
            foreach (var @interface in interfaces)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return @interface;
                }
            }

            return null;
        }
    }
}
