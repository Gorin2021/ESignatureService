namespace ESignatureService.Interfaces.CQRS.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}