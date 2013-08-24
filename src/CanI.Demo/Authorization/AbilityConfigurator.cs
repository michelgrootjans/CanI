using System.Security.Principal;
using CanI.Core;

namespace CanI.Demo.Authorization
{
    public class AbilityConfigurator : IAbilityConfigurator
    {
        private readonly IPrincipal principal;

        public AbilityConfigurator(IPrincipal principal)
        {
            this.principal = principal;
        }

        public void Configure(IAbilityConfiguration configuration)
        {
            configuration.AllowTo("view", "home");

            if (principal.IsInRole("admin"))
                configuration.AllowTo("manage", "all");

            if (principal.IsInRole("manager"))
                configuration.Allow("manage").On("customer", "customers");

            if (principal.IsInRole("callcenter"))
                configuration.Allow("view", "edit").On("customer", "customers");

            if (principal.IsInRole("viewer"))
                configuration.Allow("view").On("customer", "customers");

            configuration.IgnoreSubjectPostfix("ViewModel");
        }
    }
}