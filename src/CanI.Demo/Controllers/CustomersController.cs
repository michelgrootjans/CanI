using System.Linq;
using System.Web.Mvc;
using CanI.Demo.Domain;
using CanI.Demo.Services;

namespace CanI.Demo.Controllers
{
    public class CustomersController : Controller
    {
        // I know about DI, and I use it exclusively in all my projects.
        // However that is not the focus of this demo
        private readonly CustomerService service = new CustomerService(); 
        private readonly IRepository repository = new StaticInMemoryRepository(); 

        public ActionResult Index()
        {
            var customers = repository.FindAll<Customer>()
                                      .Select(Map);

            return View(customers);
        }

        public ActionResult Detail(int id)
        {
            var customer = repository.Find<Customer>(c => c.Id == id);
            var viewModel = Map(customer);
            return View(viewModel);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customer = repository.Find<Customer>(c => c.Id == id);
            var viewModel = Map(customer);
            return View(viewModel);
        }

        public ActionResult Create(string name)
        {
            var id = service.Create(name);
            return RedirectToAction("Detail", new {id});
        }

        public ActionResult Update(int id, string name)
        {
            service.Update(id, name);
            return RedirectToAction("Detail", new {id});
        }

        public ActionResult Delete(int id)
        {
            service.Remove(id);
            return RedirectToAction("Index");
        }

        private static CustomerViewModel Map(Customer c)
        {
            return new CustomerViewModel { Id = c.Id, Name = c.Name, CanDelete = c.CanDelete };
        }
    }

}
