using System.Web.Mvc;
using CanI.Demo.Controllers;

namespace CanI.Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeWithCanIAttribute());
        }
    }
}