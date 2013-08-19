using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CanI.Demo.Controllers;

namespace CanI.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            Func<IMvcAbility> abiltiyFactory = () => new DemoAbility(new DummyUser("viewer"));
            filters.Add(new AuthorizeWithCanIAttribute(abiltiyFactory));
            CanIHelper.ConfigureWith(abiltiyFactory);
        }
    }

    public class DemoAbility : IMvcAbility
    {
        private readonly IPrincipal principal;

        public DemoAbility(IPrincipal principal)
        {
            this.principal = principal;
        }

        public ActionResult OnAuthorizationFailed()
        {
            return new RedirectResult("/");
        }

        public bool Can(string action, string subject)
        {
            if (principal.IsInRole("admin"))
                return true;

            if (principal.IsInRole("home-owner"))
                return subject == "Home";

            return action == "Index" && subject == "Home";
        }
    }

    public class DummyUser : IPrincipal, IIdentity
    {
        private readonly string[] roles;

        public DummyUser(params string[] roles)
        {
            this.roles = roles;
        }

        public bool IsInRole(string role)
        {
            return roles.Contains(role);
        }

        public IIdentity Identity { get { return this; }}
        public string Name { get { return "Dummy User";} }
        public string AuthenticationType { get { return "dummy";} }
        public bool IsAuthenticated { get { return true;} }
    }
}