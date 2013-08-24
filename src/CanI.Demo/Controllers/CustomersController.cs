using System.Web.Mvc;
using CanI.Demo.Services;

namespace CanI.Demo.Controllers
{
    public class CustomersController : Controller
    {
        private static readonly CustomerService Service = new CustomerService(); // I know about DI, this is not the focus of this demo

        public ActionResult Index()
        {
            var customers = Service.All();
            return View(customers);
        }

        public ActionResult Detail(int id)
        {
            var customer = Service.Find(id);
            return View(customer);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customer = Service.Find(id);
            return View(customer);
        }

        public ActionResult Create(string name)
        {
            var id = Service.Create(name);
            return RedirectToAction("Detail", new {id});
        }

        public ActionResult Update(int id, string name)
        {
            Service.Update(id, name);
            return RedirectToAction("Detail", new {id});
        }
    }

}
