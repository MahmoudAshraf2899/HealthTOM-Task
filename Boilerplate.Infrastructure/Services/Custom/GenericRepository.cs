using Boilerplate.Contracts.Helpers;
using Boilerplate.Contracts.IServices.Custom;
using Boilerplate.Core.Entities;
using Boilerplate.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Boilerplate.Infrastructure.Services.Custom
{
    public partial class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Basic Functions
        private readonly DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        private IEntityType GetEntityType()
        {
            return _context.Model.FindEntityType(typeof(TEntity));
        }
        public string? GetSchema()
        {
            return GetEntityType().GetSchema();
        }
        public string? GetTableName()
        {
            return GetEntityType().GetTableName();
        }
        public string? GetPrimaryKeyColmunName()
        {
            return GetEntityType().GetKeys().Select(p => p.GetName()).FirstOrDefault();
        }
        public DbSet<TEntity> DbSet()
        {
            return _context.Set<TEntity>();
        }
        public DbSet<BaseEntityUpdate> DbSetBase()
        {
            return _context.Set<BaseEntityUpdate>();
        }
        public IQueryable<TEntity> Query()
        {
            return DbSet().AsQueryable();
        }
        public void SetContextState(TEntity entity, EntityState state)
        {
            _context.Entry(entity).State = state;
        }

        #endregion

        #region Add
        public TEntity Add(TEntity entity)
        {
            DbSet().Add(entity);
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet().AddAsync(entity);
            return entity;
        }
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            DbSet().AddRange(entities);
            return entities;
        }
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet().AddRangeAsync(entities);
            return entities;
        }
        public async Task<TEntity> AddFromMigration(TEntity entity)
        {

            var type = entity.GetType();
            var IsPublished = type.GetProperty("IsPublished");
            if (IsPublished != null) IsPublished.SetValue(entity, true);
            var IsDeleted = type.GetProperty("IsDeleted");
            if (IsDeleted != null) IsDeleted.SetValue(entity, false);
            var Status = type.GetProperty("Status");
            if (Status != null) Status.SetValue(entity, 3);
            var CreatedBy = type.GetProperty("CreatedBy");
            if (CreatedBy != null) CreatedBy.SetValue(entity, UserDetails.userId);
            var UpdatedBy = type.GetProperty("UpdatedBy");
            if (UpdatedBy != null) UpdatedBy.SetValue(entity, UserDetails.userId);
            var CreatedAt = type.GetProperty("CreatedAt");
            if (CreatedAt != null) CreatedAt.SetValue(entity, DateTime.Now);
            var UpdatedAt = type.GetProperty("UpdatedAt");
            if (UpdatedAt != null) UpdatedAt.SetValue(entity, DateTime.Now);
            DbSet().Add(entity);
            return entity;
        }
        #endregion

        #region Update
        public TEntity Update(TEntity entity)
        {
            _context.Update(entity);
            return entity;
        }
        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
            return entities;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            return entity;
        }
        #endregion

        #region Publish
        public async Task<TEntity> PublishOrUnPublish(TEntity entity, bool status)
        {
            var type = entity.GetType();
            var property = type.GetProperty("IsPublished");
            if (property != null) property.SetValue(entity, status);
            var propertyDate = type.GetProperty("PublishDate");
            if (propertyDate != null) propertyDate.SetValue(entity, DateTime.Now);
            _context.Update(entity);
            return entity;
        }
        public async Task<TEntity> Publish(TEntity entity)
        {
            return await PublishOrUnPublish(entity, true);
        }
        public async Task<TEntity> UnPublish(TEntity entity)
        {
            return await PublishOrUnPublish(entity, false);
        }

        #endregion

        #region Delete
        public void Delete(object id)
        {
            var element = DbSet().Find(id);
            Delete(element);
        }
        public void DeleteRange(IEnumerable<object> ids)
        {
            List<TEntity> lTEntity = new List<TEntity>();
            foreach (int id in ids)
                lTEntity.Add(DbSet().Find(id));
            DeleteRange(lTEntity);

        }
        public void Delete(TEntity entity)
        {
            DbSet().Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet().RemoveRange(entities);
        }
        #endregion

        #region Soft Delete
        public async Task<bool> DeleteSoftAsync(TEntity entity)
        {
            try
            {
                var type = entity.GetType();
                var property = type.GetProperty("IsDeleted");
                if (property != null) property.SetValue(entity, true);
                _context.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> CancelDeleteSoftAsync(TEntity entity)
        {
            try
            {
                var type = entity.GetType();
                var property = type.GetProperty("IsDeleted");
                if (property != null) property.SetValue(entity, false);
                _context.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Check Exist
        public async Task<bool> CheckIfEntityExist(Expression<Func<TEntity, bool>> criteria)
        {
            IQueryable<TEntity> query = DbSet();
            query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(criteria) != null ? true : false;
        }

        #endregion

        #region Attach
        public void Attach(TEntity entity)
        {
            DbSet().Attach(entity);
        }
        public void AttachRange(IEnumerable<TEntity> entities)
        {
            DbSet().AttachRange(entities);
        }

        #endregion

        #region count
        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }
        #endregion

        #region Find
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            IQueryable<TEntity> query = DbSet();
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(string[] includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.ToList();
        }
        public IEnumerable<TEntity> FindAllNoTrack(Expression<Func<TEntity, bool>> criteria, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            return query.Where(criteria).ToList();
        }
        public TEntity Find(Expression<Func<TEntity, bool>> criteria, string[] includes = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet();


            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);
        }
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null, bool disableTracking = false)
        {
            IQueryable<TEntity> query = DbSet();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null, Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            try
            {
                var x = await query.FirstOrDefaultAsync();
                return x;

            }
            catch (Exception ex)
            {

                throw;
            }
            /*
               IQueryable<TEntity> query = _context.Set<TEntity>().Where(criteria);
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
             */

        }
        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, string[] includes = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            return query.Where(criteria).ToList();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null)
        {
            IQueryable<TEntity> query = DbSet();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null, Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(criteria);
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(string[] includes = null, Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<TEntity> query = DbSet();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null, int? take = null, int? skip = null,
            Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(criteria);

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllFilterAsync(Expression<Func<TEntity, bool>> criteria)
        {
            IQueryable<TEntity> query = DbSet();
            return await query.Where(criteria).ToListAsync();
        }

        #endregion

    }
}
