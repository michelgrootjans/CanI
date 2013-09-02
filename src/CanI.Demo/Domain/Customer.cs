namespace CanI.Demo.Domain
{
    public class Customer
    {
        private static int counter = 1;

        public int Id { get; private set; }
        public string Name { get; set; }
        public bool CanDelete { get; set; }

        public Customer(string name)
        {
            Id = counter++;
            Name = name;
            CanDelete = true;
        }

    }
}