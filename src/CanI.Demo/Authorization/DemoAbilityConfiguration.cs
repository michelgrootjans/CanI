using System.Security.Principal;
using System.Web.Mvc;
using CanI.Mvc;

namespace CanI.Demo.Authorization
{
    public class DemoAbilityConfiguration : IMvcAbilityConfiguration
    {
        private readonly IPrincipal principal;

        public DemoAbilityConfiguration(IPrincipal principal)
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
}