using BookService.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Create the DI services
            var services = new ServiceCollection();

            services.AddScoped<BookServiceContext>();
            services.AddMvcControllersAsServices();

            var resolver = new DependencyResolver(services.BuildServiceProvider());

            // Resolver for Web API
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Resolver for MVC
            System.Web.Mvc.DependencyResolver.SetResolver(resolver);
        }
    }
}
