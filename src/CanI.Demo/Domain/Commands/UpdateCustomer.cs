namespace CanI.Demo.Domain.Commands
{
    internal class UpdateCustomerCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand>
    {
        public void Handle(UpdateCustomerCommand command)
        {
            new StaticInMemoryRepository()
                .FindOne<Customer>(c => c.Id == command.Id)
                .Name = command.Name;
        }
    }
}