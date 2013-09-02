using System;
using CanI.Core.Authorization;
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
            if (!I.CanExecute(command))
                throw new Exception(string.Format("You are not allowed to execute a {0}", command.GetType().Name));
            commandDispatcher.Dispatch(command);
        }
    }
}