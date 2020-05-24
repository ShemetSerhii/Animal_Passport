using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnimalPassport.Entities.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace AnimalPassport.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sorter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null);

        Guid Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(Guid id);

    }
}