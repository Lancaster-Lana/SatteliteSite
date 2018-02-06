using System;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using Sattelite.Data.Infras;
using Sattelite.Data.Infras.Specification;
using Sattelite.Entities;
using System.Data.Objects;
using Sattelite.Data;

namespace Sattelite.EntityFramework.Repository
{
    /// <summary>
    /// Generic repository
    /// </summary>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region variables & properties
        //private readonly PluralizationService _pluralizer = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));

        private readonly string _connectionStringName;
        private DbContext _context;
        protected DbContext DbContext
        {
            get
            {
                if (this._context == null)
                {
                    if (this._connectionStringName == string.Empty)
                        this._context = DbContextManager.Current;
                    else
                        this._context = DbContextManager.CurrentFor(this._connectionStringName);
                }
                return this._context;
            }
        }

        private IUnitOfWork unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this.DbContext);
                }
                return unitOfWork;
            }
        }

        #endregion

        #region Ctors

        public GenericRepository() : this(string.Empty)
        {
        }

        public GenericRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public GenericRepository(System.Data.Entity.Core.Objects.ObjectContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = new DbContext(context, true);
        }

        #endregion

        #region Methods

        public TEntity GetByKey(object keyValue)
        {
            var originalItem = DbContext.Set<TEntity>().Find(keyValue);
            return originalItem != null ? originalItem : default(TEntity);
        }

        /// <summary>
        /// to be provste
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQuery()
        {
            return DbContext.Set<TEntity>();
        }

        public IQueryable<SubEntity> GetQuery<SubEntity>() where SubEntity : Entity
        {
            return DbContext.Set<SubEntity>();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().Where(predicate);
        }

        public IQueryable<TEntity> GetQuery(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery());
        }


        public IEnumerable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetQuery(criteria).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetQuery(criteria).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<TEntity> Get<TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return specification.SatisfyingEntitiesFrom(GetQuery()).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return specification.SatisfyingEntitiesFrom(GetQuery()).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        //public TEntity Single(Expression<Func<TEntity, bool>> criteria)
        //{
        //    return GetQuery().Single(criteria);
        //}

        //public TEntity Single(ISpecification<TEntity> criteria)
        //{
        //    return criteria.SatisfyingEntityFrom(GetQuery());
        //}

        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().First(predicate);
        }

        public TEntity First(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).First();
        }

        public IEnumerable<TEntity> GetIncluding(Expression<Func<TEntity, bool>> criteria, params string[] linkedEntities)
        {
            var query = GetQuery(criteria);

            if (linkedEntities != null)
                foreach (var linkedEntity in linkedEntities)
                    query = query.Include(linkedEntity);

            return query;
        }

        public TEntity GetOne(Expression<Func<TEntity, bool>> criteria)
        {
            return Get(criteria).FirstOrDefault();
        }

        public TEntity GetOne(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntityFrom(GetQuery());
        }

        public TEntity GetOneIncluding(Expression<Func<TEntity, bool>> criteria, params string[] linkedEntities)
        {
            return GetIncluding(criteria, linkedEntities).FirstOrDefault();
        }

        public TEntity GetOne<TProperty>(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, TProperty>>[] linkedEntities) where TProperty : class//IEnumerable<Entity>
        {
            return GetIncluding(criteria, linkedEntities).FirstOrDefault();
        }

        public IEnumerable<TEntity> Get(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).AsEnumerable();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Where(criteria);
        }

        /// <summary>
        /// Include child entities
        /// </summary>
        /// <typeparam name="TProperty"> type of related entities to be included as details in the TEntity </typeparam>
        /// <param name="criteria"></param>
        /// <param name="linkedEntities"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetIncluding<TProperty>(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, TProperty>>[] linkedEntities) where TProperty : class//IEnumerable<Entity>
        {
            var query = GetQuery(criteria);

            if (linkedEntities != null)
                foreach (var linkedEntity in linkedEntities)
                    query = query.Include(linkedEntity);

            return query;
        }

        public IEnumerable<TEntity> GetAll<TProperty>(params Expression<Func<TEntity, TProperty>>[] linkedCollectionsEntities) where TProperty : IEnumerable<Entity>
        {
            var query = GetQuery();
            if (linkedCollectionsEntities != null)
                foreach (var linkedEntity in linkedCollectionsEntities)
                    query = query.Include(linkedEntity);

            return query;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        /// <summary>
        /// Attach entitity to context
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
        }

        private void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.CreatedDate = DateTime.Now;

            DbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
            //DbContext.Set<TEntity>().Add(entity);
        }

        private void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now; //Update modified date

            //DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Add or update - depending on TEntity Id key
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Save(TEntity entity)
        {
            if (entity.Id > 0)
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }

            DbContext.SaveChanges();
            UnitOfWork.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", string.Format("Empty {0} cannot be deleted !", typeof(TEntity).Name));
            }
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// Delete Child Entity (from linked TEntity collection)
        /// NOTE: if there no reason in creating additional repository
        /// </summary>
        /// <typeparam name="TSubEntity"></typeparam>
        /// <param name="entity"></param>
        public void Delete<TSubEntity>(TSubEntity entity) where TSubEntity : Entity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", string.Format("Empty {0} cannot be deleted !", typeof(TEntity).Name));
            }
            DbContext.Set<TSubEntity>().Remove(entity);
            DbContext.SaveChanges();
            UnitOfWork.SaveChanges();
        }

        public void Delete(Expression<Func<TEntity, bool>> criteria)
        {
            IEnumerable<TEntity> records = Get(criteria);

            foreach (TEntity record in records)
            {
                Delete(record);
            }
            DbContext.SaveChanges();
            UnitOfWork.SaveChanges();
        }

        public void Delete(ISpecification<TEntity> criteria)
        {
            IEnumerable<TEntity> records = Get(criteria);
            foreach (TEntity record in records)
            {
                Delete(record);
            }
            DbContext.SaveChanges();
        }


        public int Count()
        {
            return GetQuery().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Count(criteria);
        }

        public int Count(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(GetQuery()).Count();
        }

        #endregion
    }
}
