namespace Sattelite.Web
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Sattelite.Data;
    using Sattelite.Web.App_Start;
    using WebMatrix.WebData;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
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

            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection(CONSTS.DefaultConnectionString, "User", "Id", "UserName", autoCreateTables: true);
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