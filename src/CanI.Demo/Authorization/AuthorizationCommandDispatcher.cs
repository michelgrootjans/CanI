using CanI.Demo.Domain.Commands;

namespace CanI.Demo.Authorization
{
    internal class AuthorizationCommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandDispatcher commandDispatcher;

        public AuthorizationCommandDispatcher(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            commandDispatcher.Dispatch(command);
        }
    }
}