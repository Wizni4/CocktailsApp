using MediatR;


namespace CocktailsApp.Application.Common
{
    /// <summary>
    /// Represents a query handler that processes queries of type <typeparamref name="TQuery"/> 
    /// and returns a DTO of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    /// <typeparam name="TResult">The type of DTO returned by the query handler.</typeparam>
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>;
}
