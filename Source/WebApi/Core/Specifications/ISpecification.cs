using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DrakeLambert.Peerra.WebApi.Core.Entities;

namespace DrakeLambert.Peerra.WebApi.Core.Specifications
{
    public interface ISpecification<TEntity> where TEntity : IEntity
    {
        IReadOnlyCollection<Expression<Func<TEntity, bool>>> Criteria { get; }

        Expression<Func<TEntity, bool>> ComposedCriteria { get; }
    }
}
