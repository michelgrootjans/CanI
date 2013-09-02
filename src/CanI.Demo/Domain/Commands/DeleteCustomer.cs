namespace CanI.Demo.Domain.Commands
{
    internal class DeleteCustomerCommand
    {
        public int Id { get; set; }
    }

    internal class DeleteCustomerHandler : ICommandHandler<DeleteCustomerCommand>
    {
        public void Handle(DeleteCustomerCommand command)
        {
            new StaticInMemoryRepository()
                .Remove<Customer>(c => c.Id == command.Id);
        }
    }
}