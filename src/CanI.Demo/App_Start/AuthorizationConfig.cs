using CanI.Core;
using CanI.Demo.Authorization;

namespace CanI.Demo.App_Start
{
    public static class AuthorizationConfig
    {
        public static void Configure()
        {
            AbilityConfiguration.ConfigureWith(
                config => new AbilityConfigurator(config, System.Web.HttpContext.Current.User)
            );
        }
    }
}