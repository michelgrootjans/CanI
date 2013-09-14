using System.Web.Mvc;

namespace CanI.Demo.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // set authorization over all controllers at once
            // filters.Add(new AuthorizeWithCanIFilter(context => new RedirectResult("/")));
        }
    }
}