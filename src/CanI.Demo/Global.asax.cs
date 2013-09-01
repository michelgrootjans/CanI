using System;
using System.Linq;
using System.Security.Principal;
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

    public class DummyUser : IPrincipal, IIdentity
    {
        private readonly string name;

        public DummyUser(string name, params string[] roles)
        {
            this.name = name;
            this.Roles = roles;
        }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public IIdentity Identity { get { return this; } }
        public string Name { get { return name; } }
        public string AuthenticationType { get { return "dummy"; } }
        public bool IsAuthenticated { get { return true; } }
        internal string[] Roles { get; private set; }
    }

    public static class PrincipalExtensions
    {
        public static string PrintRoles(this IPrincipal principal)
        {
            var user = principal as DummyUser;
            return user == null ? "N/A" : string.Join(", ", user.Roles);
        }
    }
}