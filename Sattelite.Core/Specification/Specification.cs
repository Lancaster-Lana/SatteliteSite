// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Specification.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the Specification type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Sattelite.Data.Infras.Specification
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Sattelite.Data.Infras.Extensions;

    public class Specification<TEntity> : ISpecification<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Specification{TEntity}"/> class.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            this.Predicate = predicate;
        }


        public Specification<TEntity> And(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.And(specification.Predicate));
        }

        public Specification<TEntity> And(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.And(predicate));
        }

        public Specification<TEntity> Or(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.Or(specification.Predicate));
        }

        public Specification<TEntity> Or(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.Or(predicate));
        }


        public TEntity SatisfyingEntityFrom(IQueryable<TEntity> query)
        {
            return query.Where(this.Predicate).SingleOrDefault();
        }

        public IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query)
        {
            return query.Where(this.Predicate);
        }

        public Expression<Func<TEntity, bool>> Predicate;
    }
}
