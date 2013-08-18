using System;
using System.Web.Mvc;

namespace CanI.Demo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SecretData()
        {
            return View();
        }

    }

    public class AuthorizeWithCanIAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Console.WriteLine(filterContext.ActionDescriptor);
        }
    }
}
