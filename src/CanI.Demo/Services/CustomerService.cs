using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CanI.Demo.Services
{
    internal class CustomerService
    {
        readonly List<CustomerViewModel> customers;

        public CustomerService()
        {
            customers = new List<CustomerViewModel>
            {
                new CustomerViewModel("Apple"),
                new CustomerViewModel("Microsoft"){CanDelete = false},
                new CustomerViewModel("Amazon"),
                new CustomerViewModel("Dropbox")
            };
        }

        public IEnumerable<CustomerViewModel> All()
        {
            return customers;
        }

        public CustomerViewModel Find(int id)
        {
            return customers.Find(c => c.Id == id);
        }

        public void Update(int id, string name)
        {
            Find(id).Name = name;
        }

        public int Create(string name)
        {
            var customer = new CustomerViewModel(name);
            customers.Add(customer);
            return customer.Id;
        }

        public void Remove(int id)
        {
            customers.RemoveAll(c => c.Id == id);
        }
    }

    public class CustomerViewModel
    {
        private static int counter;

        public int Id { get; private set; }
        public string Name { get; internal set; }
        public bool CanDelete { get; internal set; }

        public CustomerViewModel(string name)
        {
            Id = counter++;
            Name = name;
            CanDelete = true;
        }
    }

}