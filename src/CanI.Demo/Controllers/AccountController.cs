using System.Web.Mvc;
using System.Web.Security;

namespace CanI.Demo.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogIn(string roleName)
        {
            FormsAuthentication.SetAuthCookie(roleName, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
