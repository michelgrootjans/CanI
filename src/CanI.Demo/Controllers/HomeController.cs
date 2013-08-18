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

    }
}
