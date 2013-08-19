using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using CanI.Demo.Authorization;
using CanI.Demo.Controllers;
using CanI.Mvc;

namespace CanI.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            Func<IMvcAbility> abiltiyFactory = () => new DemoAbility(new DummyUser("admin"));
            filters.Add(new AuthorizeWithCanIAttribute(abiltiyFactory));
            CanIHelper.ConfigureWith(abiltiyFactory);
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