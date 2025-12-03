using ESignatureService.Interfaces.CQRS.Commands;

namespace ESignatureService.Common.CQRS.Command
{
    public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
    {
        public Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            object? service = serviceProvider.GetService(typeof(ICommandHandler<TCommand>));
            if (service == null)
                throw new ArgumentNullException(Constant.DEPENDENCY_RESOLVER_ERROR);
            return ((ICommandHandler<TCommand>)service).ExecuteAsync(command);
        }
    }
}
