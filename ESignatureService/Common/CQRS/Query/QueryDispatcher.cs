using ESignatureService.Interfaces.CQRS.Queries;

namespace ESignatureService.Common.CQRS.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        public IServiceProvider ServiceProvider { get; }

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var queryHandler = ServiceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

            if (queryHandler == null)
                throw new NullReferenceException($"{Constant.DEPENDENCY_RESOLVER_ERROR} '{typeof(IQueryHandler<TQuery, TResult>)}'.");

            return await ((IQueryHandler<TQuery, TResult>)queryHandler).ExecuteAsync(query);
        }
    }
}