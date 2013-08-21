using System.Web.Mvc;
using CanI.Demo.Authorization;
using CanI.Mvc;

namespace CanI.Demo.App_Start
{
    public static class AuthorizationConfig
    {
        public static void Configure()
        {
            CanIMvcConfiguration.ConfigureWith(
                () => new AbilityConfigurator(new DummyUser("admin")),
                () => new RedirectResult("/")
                );
        }
    }
}