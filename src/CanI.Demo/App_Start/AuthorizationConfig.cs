using System.Diagnostics;
using CanI.Core.Configuration;
using CanI.Demo.Authorization;
using CanI.Mvc;

namespace CanI.Demo
{
    public static class AuthorizationConfig
    {
        public static void Configure()
        {
            AbilityConfiguration.Debug(message => Trace.Write(string.Format("Authorization: {0}", message))).Verbose();
            AbilityConfiguration.ConfigureCache(new PerRequestHttpCache());
            AbilityConfiguration.ConfigureWith(
                config => new AbilityConfigurator(config, System.Web.HttpContext.Current.User)
            );
        }
    }
}