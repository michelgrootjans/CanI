using System.Web.Mvc;
using System.Web.Security;

namespace CanI.Demo.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string userName)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
