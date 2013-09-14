using System.Security.Principal;
using CanI.Core.Configuration;

namespace CanI.Demo.Authorization
{
    public class AbilityConfigurator
    {
        public AbilityConfigurator(IAbilityConfiguration config, IPrincipal principal)
        {
            if (principal.IsInRole("admin"))
                config.AllowAll().OnEverything();

            if (principal.IsInRole("manager"))
                config.AllowAll().On("Customer");

            if (principal.IsInRole("callcenter"))
                config.Allow("View", "Edit").On("Customer");

            if (principal.IsInRole("viewer"))
                config.Allow("View").On("Customer");

            config.ConfigureSubjectAliases("Customer", "Customers");
        }
    }
}