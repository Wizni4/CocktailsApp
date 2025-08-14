using MediatR;

namespace CocktailsApp.Application.Common
{
    public interface IQuery : IBaseRequest;
    /// <summary>
    /// Represents a typed query against a domain entity.
    /// </summary>
    /// <typeparam name="TDomain">The type of the domain entity targeted by the query.</typeparam>
    public interface IQuery<TResult> : IQuery, IRequest<TResult>;
}
