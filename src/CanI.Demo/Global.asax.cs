using System;
using System.Web;
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

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) return;

            HttpContext.Current.User = new DummyUser(HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name);
        }
    }
}