using System.Web.Mvc;
using CanI.Core.Authorization;
using CanI.Mvc;

namespace CanI.Demo.Controllers
{
    [AuthorizeWithCanIFilter]
    public class AdminController : Controller
    {
        [AuthorizeIfICan("eat", "hamburger")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
