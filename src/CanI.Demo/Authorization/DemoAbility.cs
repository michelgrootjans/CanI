using System.Security.Principal;
using System.Web.Mvc;
using CanI.Core;
using CanI.Mvc;

namespace CanI.Demo.Authorization
{
    public class DemoAbilityConfiguration : IAbilityConfigurator
    {
        private readonly IPrincipal principal;

        public DemoAbilityConfiguration(IPrincipal principal)
        {
            this.principal = principal;

        }

        public void Configure(IAbilityConfiguration userConfiguration)
        {
            if (principal.IsInRole("admin"))
                userConfiguration.AllowTo("manage", "all");

            if (principal.IsInRole("home-owner"))
                userConfiguration.AllowTo("manage", "home");

            userConfiguration.AllowTo("view", "home");
        }

//        public ActionResult OnAuthorizationFailed()
//        {
//            return new RedirectResult("/");
//        }


        //        public bool CanI(string action, string subject)

//        {

//            if (principal.IsInRole("admin"))

//                return true;

//            if (principal.IsInRole("home-owner"))

//                return subject == "Home";

//            return action == "Index" && subject == "Home";

//        }
    }
}