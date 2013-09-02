using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CanI.Demo.Services
{
    internal class CustomerService
    {
        readonly List<CustomerViewModel> customers;

        public CustomerService()
        {
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
            var customer = new CustomerViewModel();
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

        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanDelete { get; set; }
    }

}