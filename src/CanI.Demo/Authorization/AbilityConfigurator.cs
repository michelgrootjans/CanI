using CanI.Core;

namespace CanI.Demo.Authorization
{
    public class AbilityConfigurator
    {
        public AbilityConfigurator(IAbilityConfiguration config)
        {
            var principal = System.Web.HttpContext.Current.User;

            config.Allow("Login", "LogOff").On("Account");
            config.AllowTo("View", "Home");

            if (principal.IsInRole("admin"))
                config.AllowTo("Manage", "All");

            if (principal.IsInRole("manager"))
                config.AllowTo("Manage", "Customer");

            if (principal.IsInRole("callcenter"))
                config.Allow("View", "Edit").On("Customer");

            if (principal.IsInRole("viewer"))
                config.Allow("View").On("Customer");

            config.IgnoreSubjectPostfix("ViewModel");
            config.ConfigureSubjectAliases("Customer", "Customers");
        }
    }
}