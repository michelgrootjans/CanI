using System.Web.Mvc;
using System.Web.Routing;
using CanI.Demo.App_Start;

namespace CanI.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AuthorizationConfig.Configure();
        }
    }
}