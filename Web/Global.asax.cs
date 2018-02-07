namespace Sattelite.Web
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Sattelite.Web.App_Start;
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AppConfig.Run(); //removed autofac

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            var builder = new ContainerBuilder();
            builder.RegisterModule<WebModule>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Application["users_count"] = Convert.ToInt32(Application["users_count"]) + 1;
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            if (Application["users_count"] != null)
                Application["users_count"] = Convert.ToInt32(Application["users_count"]) - 1;
        }
    }
}