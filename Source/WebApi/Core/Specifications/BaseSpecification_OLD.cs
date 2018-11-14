// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using Ardalis.GuardClauses;
// using DrakeLambert.Peerra.WebApi.Core.Entities;

// namespace DrakeLambert.Peerra.WebApi.Core.Specifications
// {
//     public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : IEntity
//     {
//         private List<Expression<Func<TEntity, bool>>> _criteria = new List<Expression<Func<TEntity, bool>>>();

//         public IReadOnlyCollection<Expression<Func<TEntity, bool>>> Criteria => _criteria.AsReadOnly();

//         public Expression<Func<TEntity, bool>> ComposedCriteria
//         {
//             get
//             {
//                 Expression<Func<TEntity, bool>> defaultExpression = entity => true;

//                 foreach (var criterion in _criteria)
//                 {
//                     var invokedExpression = Expression.Invoke(criterion, defaultExpression.Parameters.Cast<Expression>());

//                     defaultExpression = Expression.Lambda<Func<TEntity, bool>>(Expression.And(defaultExpression.Body, invokedExpression), defaultExpression.Parameters);
//                 }

//                 return defaultExpression;
//             }
//         }

//         public BaseSpecification(params Expression<Func<TEntity, bool>>[] criteria)
//         {
//             foreach (var criterion in criteria)
//             {
//                 Guard.Against.Null(criterion, nameof(criteria));
//             }
//             _criteria.AddRange(criteria);
//         }
//     }
// }
