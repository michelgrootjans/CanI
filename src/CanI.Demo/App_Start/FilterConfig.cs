using System.Web.Mvc;
using CanI.Demo.Authorization;
using CanI.Mvc;

namespace CanI.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            CanIMvcConfiguration.ConfigureWith(
                () => new DemoAbilityConfigurator(new DummyUser("admin")),
                () => new RedirectResult("/")
                );

            filters.Add(new AuthorizeWithCanIAttribute());
        }
    }
}