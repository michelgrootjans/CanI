namespace CanI.Demo.Domain.Commands
{
    internal class CreateCustomerCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand>
    {
        public void Handle(CreateCustomerCommand command)
        {
            var customer = new Customer(command.Name);
            new StaticInMemoryRepository().Add(customer);
            command.Id = customer.Id; // I know, I know, CQRS and return types ... This is just a demo!
        }
    }

}