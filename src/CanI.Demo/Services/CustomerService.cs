using System.Collections.Generic;

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
                new CustomerViewModel("Microsoft"),
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
    }

    public class CustomerViewModel
    {
        private static int counter;

        public int Id { get; private set; }
        public string Name { get; internal set; }

        public CustomerViewModel(string name)
        {
            Id = counter++;
            Name = name;
        }
    }

}