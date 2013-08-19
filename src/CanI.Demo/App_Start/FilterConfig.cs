using System.Web.Mvc;
using CanI.Core;
using CanI.Demo.Authorization;
using CanI.Mvc;

namespace CanI.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            CanIConfiguration.ConfigureWith(() => new DemoAbility(new DummyUser("admin")));

            filters.Add(new AuthorizeWithCanIAttribute());
        }
    }
}