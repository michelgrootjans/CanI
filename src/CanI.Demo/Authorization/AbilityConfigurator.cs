using System.Diagnostics;
using System.Security.Principal;
using CanI.Core.Configuration;

namespace CanI.Demo.Authorization
{
    public class AbilityConfigurator
    {
        public AbilityConfigurator(IAbilityConfiguration config, IPrincipal principal)
        {
            Trace.Write(string.Format("Checking abilities of: {0}", principal.Identity.IsAuthenticated ? principal.Identity.Name : "unauthenticated user"));
            if (principal.IsInRole("admin"))
                config.AllowAnything().OnEverything();

            if (principal.IsInRole("manager"))
                config.AllowAnything().On("Customer");

            if (principal.IsInRole("callcenter"))
                config.Allow("View", "Edit").On("Customer");

            if (principal.IsInRole("viewer"))
                config.Allow("View").On("Customer");

            config.ConfigureSubjectAliases("Customer", "Customers");
        }
    }
}