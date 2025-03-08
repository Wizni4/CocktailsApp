/*
 * Framework namespaces
 */
using System;
using System.Linq.Expressions;

namespace Domain.SeedWork
{
    public abstract class ByIdSpecification<T> : Specification<T> where T : Entity
    {
        private readonly Guid _id;

        public ByIdSpecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                return entity => entity.Id == _id;
            }
        }
    }
}