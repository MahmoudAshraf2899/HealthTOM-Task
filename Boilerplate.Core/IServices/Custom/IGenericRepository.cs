using Boilerplate.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Boilerplate.Contracts.IServices.Custom
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Basic Functions
        string? GetSchema();
        string? GetTableName();
        string? GetPrimaryKeyColmunName();
        DbSet<TEntity> DbSet();
        //DbSet<BaseEntityUpdate> DbSetBase();
        IQueryable<TEntity> Query();
        void SetContextState(TEntity entity, EntityState state);

        #endregion

        #region Add
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        Task<TEntity> AddFromMigration(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Update
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        #endregion

        #region Delete
        void Delete(object id);
        void DeleteRange(IEnumerable<object> ids);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        #endregion

        #region Soft Delete
        Task<bool> DeleteSoftAsync(TEntity entity);
        Task<bool> CancelDeleteSoftAsync(TEntity entity);
        #endregion

        #region Publish 
        Task<TEntity> Publish(TEntity entity);
        Task<TEntity> UnPublish(TEntity entity);
        #endregion

        #region Check Exist
        Task<bool> CheckIfEntityExist(Expression<Func<TEntity, bool>> criteria);
        #endregion

        #region Attach
        void Attach(TEntity entity);
        void AttachRange(IEnumerable<TEntity> entities);
        #endregion

        #region Count
        Task<int> CountAsync();
        #endregion

        #region Find
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<IEnumerable<TEntity>> FindAllAsync(string[] includes = null);
        IEnumerable<TEntity> FindAllNoTrack(Expression<Func<TEntity, bool>> criteria, bool disableTracking = true);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria = null, string[] includes = null);
        Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> criteria, string[] includes = null, bool disableTracking = true);
        Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] includes = null, Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending, bool disableTracking = true);
        TEntity Find(Expression<Func<TEntity, bool>> criteria, string[] includes = null, bool disableTracking = true);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null, bool disableTracking = false);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null, int? take = null, int? skip = null,
           Expression<Func<TEntity, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        Task<IEnumerable<TEntity>> FindAllFilterAsync(Expression<Func<TEntity, bool>> criteria);
        Task<IEnumerable<TEntity>> FindAllAsync(string[] includes = null, Expression<Func<TEntity, object>> orderBy = null,
              string orderByDirection = OrderBy.Ascending);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null, Expression<Func<TEntity, object>> orderBy = null,
            string orderByDirection = OrderBy.Ascending);
        #endregion
    }
}
