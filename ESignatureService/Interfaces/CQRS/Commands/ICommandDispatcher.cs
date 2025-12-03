namespace ESignatureService.Interfaces.CQRS.Commands;

public interface ICommandDispatcher
{
    Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
}