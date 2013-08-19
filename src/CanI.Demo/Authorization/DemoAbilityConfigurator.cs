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

        public void Configure(IAbilityConfiguration configuration)
        {
            configuration.AllowTo("view", "home");

            if (principal.IsInRole("admin"))
                configuration.AllowTo("manage", "all");

            if (principal.IsInRole("home-owner"))
                configuration.AllowTo("manage", "home");
        }
    }
}