using System.Collections.Generic;
using System.Linq;

namespace CanI.Demo.Domain.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command);
    }

    internal class HardCodedCommandDispatcher : ICommandDispatcher
    {
        private readonly IEnumerable<ICommandHandler> handlers;

        public HardCodedCommandDispatcher()
        {
            handlers = new List<ICommandHandler>
            {
                new CreateCustomerHandler(),
                new UpdateCustomerHandler(),
                new DeleteCustomerHandler()
            };
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            var handler = handlers.First(h => h is ICommandHandler<TCommand>) as ICommandHandler<TCommand>;
            if (handler != null) handler.Handle(command);
        }
    }
}