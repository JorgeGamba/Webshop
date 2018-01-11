using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Webshop.Features.ProductRegistration;
using Webshop.Features.ProductSearch;

namespace Webshop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ByFeatureRazorViewEngine());

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<InMemoryStorage>(Lifestyle.Singleton);
            container.Register<FindProductsByTitleQueryFactory>(Lifestyle.Singleton);
            container.Register<ProductStoringDAOFactory>(Lifestyle.Singleton);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
