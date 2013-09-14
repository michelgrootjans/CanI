using System.Web.Mvc;
using CanI.Mvc;

namespace CanI.Demo.Controllers
{
    [AuthorizeWithCanIFilter]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
