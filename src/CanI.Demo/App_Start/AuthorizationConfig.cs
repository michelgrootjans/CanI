using System.Diagnostics;
using CanI.Core.Configuration;
using CanI.Demo.Authorization;

namespace CanI.Demo
{
    public static class AuthorizationConfig
    {
        public static void Configure()
        {
            AbilityConfiguration.Debug(message => Debug.Write(string.Format("Authorization: {0}", message))).Verbose();
            AbilityConfiguration.ConfigureWith(
                config => new AbilityConfigurator(config, System.Web.HttpContext.Current.User)
            );
        }
    }
}