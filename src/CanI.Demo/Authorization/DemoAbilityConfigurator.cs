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
            if (principal.IsInRole("admin"))
                configuration.AllowTo("manage", "all");

            if (principal.IsInRole("home-owner"))
                configuration.AllowTo("manage", "home");

            configuration.AllowTo("view", "home");
        }
    }
}