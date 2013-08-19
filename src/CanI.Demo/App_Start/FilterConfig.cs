using System.Web.Mvc;
using CanI.Demo.Controllers;

namespace CanI.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeWithCanIAttribute(new DemoAbility()));
        }
    }

    public class DemoAbility : IMvcAbility
    {
        public ActionResult OnAuthorizationFailed()
        {
            return new RedirectResult("/");
        }

        public bool Can(string action, string controller)
        {
            return action == "Index" && controller == "Home";
        }
    }
}