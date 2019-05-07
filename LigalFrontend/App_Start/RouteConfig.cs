using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace LigalFrontend
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string rutaBase = ConfigurationManager.AppSettings["rutaBase"].ToString();

            routes.MapRoute(
                name: "Default",
                //url: "{rutaBase}/{controller}/{action}/{id}",
                //defaults: new {rutaBase = rutaBase, controller = "Home", action = "Login", id = UrlParameter.Optional }
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
