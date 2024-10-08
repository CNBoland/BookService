using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookService
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddMvcControllersAsServices(this IServiceCollection services)
        {
            IEnumerable<Type> serviceTypes = typeof(ServiceProviderExtensions).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IController).IsAssignableFrom(t)
                         || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));

            foreach (var type in serviceTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}
