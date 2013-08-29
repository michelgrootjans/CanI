using System.Web.Mvc;
using CanI.Mvc;

namespace CanI.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // this is the default ActionResult on failed authorization
            filters.Add(new AuthorizeWithCanIFilter(context => new RedirectResult("/")));
        }
    }
}