using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CanI.Demo.Authorization;
using CanI.Demo.Domain;
using CanI.Demo.Domain.Commands;
using CanI.Demo.Models;

namespace CanI.Demo.Controllers
{
    public class CustomersController : Controller
    {
        // I know about DI, and I use it exclusively in all my projects.
        // However that is not the focus of this demo

        private readonly ICommandDispatcher dispatcher =
            new AuthorizationCommandDispatcher(new HardCodedCommandDispatcher());

        private readonly IRepository repository = new StaticInMemoryRepository();

        public ActionResult Index()
        {
            IEnumerable<CustomerViewModel> customers = repository.FindAll<Customer>()
                .Select(Map);

            return View(customers);
        }

        public ActionResult Detail(int id)
        {
            var customer = repository.FindOne<Customer>(c => c.Id == id);
            CustomerViewModel viewModel = Map(customer);
            return View(viewModel);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customer = repository.FindOne<Customer>(c => c.Id == id);
            CustomerViewModel viewModel = Map(customer);
            return View(viewModel);
        }

        public ActionResult Create(string name)
        {
            var command = new CreateCustomerCommand {Name = name};
            dispatcher.Dispatch(command);
            return RedirectToAction("Detail", new {id = command.Id});
        }

        public ActionResult Update(int id, string name)
        {
            dispatcher.Dispatch(new UpdateCustomerCommand {Id = id, Name = name});
            return RedirectToAction("Detail", new {id});
        }

        public ActionResult Delete(int id)
        {
            dispatcher.Dispatch(new DeleteCustomerCommand {Id = id});
            return RedirectToAction("Index");
        }

        private static CustomerViewModel Map(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                CanDelete = customer.CanDelete
            };
        }
    }
}