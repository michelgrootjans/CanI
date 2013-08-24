using System.Security.Principal;
using CanI.Core;

namespace CanI.Demo.Authorization
{
    public class AbilityConfigurator
    {
        public AbilityConfigurator(IAbilityConfiguration config, IPrincipal principal)
        {
            config.AllowTo("view", "home");

            if (principal.IsInRole("admin"))
                config.AllowTo("Manage", "All");

            if (principal.IsInRole("manager"))
                config.Allow("Manage").On("Customer", "Customers");

            if (principal.IsInRole("callcenter"))
                config.Allow("View", "Edit").On("Customer", "Customers");

            if (principal.IsInRole("viewer"))
                config.Allow("View").On("Customer", "Customers");

            config.IgnoreSubjectPostfix("ViewModel");
        }
    }
}