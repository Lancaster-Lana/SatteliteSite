
namespace Sattelite.Data.Infras
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Sattelite.Data.Infras.Specification;
    using Sattelite.Entities;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Gets entity by key.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="keyValue">The key value.</param>
        /// <returns></returns>
        TEntity GetByKey(object keyValue);// where TEntity : Entity;

        /// <summary>
        /// Gets the query for current Entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery();

        /// <summary>
        ///  Gets the query for some additional Entity
        /// </summary>
        /// <typeparam name="SubEntity"></typeparam>
        /// <returns></returns>
        IQueryable<SubEntity> GetQuery<SubEntity>() where SubEntity : Entity;

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IQueryable<TEntity> GetQuery(ISpecification<TEntity> criteria);


        /// <summary>
        /// Gets the specified order by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// Gets the specified criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);
        /// <summary>
        /// Gets entities which satifies a specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get<TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(ISpecification<TEntity> criteria);

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Finds entities with related\linked entities
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="linkedEntities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetIncluding(Expression<Func<TEntity, bool>> criteria, params string[] linkedEntities);

        /// <summary>
        /// Get entities, including related\linked entities
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="linkedEntities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetIncluding<TProperty>(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, TProperty>>[] linkedEntities) where TProperty : class;//IEnumerable<Entity>;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity GetOne(ISpecification<TEntity> criteria);

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity GetOne(Expression<Func<TEntity, bool>> criteria);

        TEntity GetOneIncluding(Expression<Func<TEntity, bool>> criteria, params string[] linkedEntities);

        TEntity GetOne<TProperty>(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, TProperty>>[] linkedEntities) where TProperty : class;

        ///// <summary>
        ///// Firsts the specified predicate.
        ///// </summary>
        ///// <typeparam name="TEntity">The type of the entity.</typeparam>
        ///// <param name="predicate">The predicate.</param>
        ///// <returns></returns>
        //TEntity First(Expression<Func<TEntity, bool>> predicate);

        ///// <summary>
        ///// Gets first entity with specification.
        ///// </summary>
        ///// <typeparam name="TEntity">The type of the entity.</typeparam>
        ///// <param name="criteria">The criteria.</param>
        ///// <returns></returns>
        //TEntity First(ISpecification<TEntity> criteria);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        //void Add(TEntity entity);// where TEntity : class;

        ///// <summary>
        ///// Attaches the specified entity. It makes its 'unmodified' in DBContext
        ///// </summary>
        ///// <typeparam name="TEntity">The type of the entity.</typeparam>
        ///// <param name="entity">The entity.</param>
        //void Attach(TEntity entity);

        /// <summary>
        /// Updates changes of the existing entity. 
        /// The caller must later call SaveChanges() on the repository explicitly to save the entity to database
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        //void Update(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);// where TEntity : class;

        /// <summary>
        /// Deletes one or many entities matching the specified criteria
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Deletes entities which satify specificatiion
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete(ISpecification<TEntity> criteria);

        void Delete<TSubEntity>(TSubEntity entity) where TSubEntity : Entity;


        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        int Count();// where TEntity : class;

        /// <summary>
        /// Counts entities with the specified criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count(ISpecification<TEntity> criteria);

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        IUnitOfWork UnitOfWork { get; }
    }
}
