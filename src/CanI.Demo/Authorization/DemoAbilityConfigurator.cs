using System.Security.Principal;
using CanI.Core;

namespace CanI.Demo.Authorization
{
    public class DemoAbilityConfigurator : IAbilityConfigurator
    {
        private readonly IPrincipal principal;

        public DemoAbilityConfigurator(IPrincipal principal)
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
    }
}