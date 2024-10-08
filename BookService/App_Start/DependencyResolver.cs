using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace BookService
{
    /// <summary>
    /// Provides the default dependency resolver for the application - based on IDependencyResolver, which has just two methods.
    /// This is combined dependency resolver for MVC and WebAPI usage.
    /// </summary>
    /// <remarks>
    /// Sources:
    /// https://stackoverflow.com/a/47850171
    /// https://stackoverflow.com/a/58783838/124448
    /// https://github.com/VeronicaWasson/BookService
    /// </remarks>
    public class DependencyResolver : System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly IServiceScope scope = null;

        public DependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public DependencyResolver(IServiceScope scope)
        {
            this.scope = scope;
            this.serviceProvider = scope.ServiceProvider;
        }

        public IDependencyScope BeginScope()
        {
            return new DependencyResolver(serviceProvider.CreateScope());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                scope?.Dispose();
            }
        }

        public object GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }
    }
}
