namespace CanI.Demo.Domain.Commands
{
    public interface ICommandHandler {}
    public interface ICommandHandler<in TCommand> : ICommandHandler
    {
        void Handle(TCommand command);
    }
}