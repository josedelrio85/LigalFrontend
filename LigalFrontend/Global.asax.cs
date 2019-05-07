using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LigalFrontend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);  
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            log.Error("app_error", ex);
        }
    }
}
