using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnimalPassport.DataAccess.Interfaces;
using AnimalPassport.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AnimalPassport.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> DbSet => _context.Set<TEntity>();

        private IQueryable<TEntity> NoTrackingDbSet => _context.Set<TEntity>().AsNoTracking();

        public Task<TEntity> GetAsync(Guid id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null)
        {
            if (includeProperties != null)
            {
                return includeProperties(NoTrackingDbSet).FirstOrDefaultAsync(x => x.Id == id);
            }

            return NoTrackingDbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sorter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            var resultSet = NoTrackingDbSet.Where(filter);

            if (sorter != null)
            {
                resultSet = resultSet.OrderBy(sorter);
            }

            if (includeProperties != null)
            {
                resultSet = includeProperties(resultSet);
            }

            if (skip.HasValue)
            {
                resultSet = resultSet.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                resultSet = resultSet.Take(take.Value);
            }

            return await resultSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null)
        {
            if (includeProperties != null)
            {
                return await includeProperties(NoTrackingDbSet).ToListAsync();
            }

            return await NoTrackingDbSet.ToListAsync();
        }

        public Guid Create(TEntity entity)
        {
            DbSet.Add(entity);

            return entity.Id;
        }

        public void Update(TEntity entity)
        {
            var existingEntity = DbSet.Find(entity.Id);
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public void Delete(Guid id)
        {
            var existingEntity = DbSet.Find(id);

            if (existingEntity == null)
            {
                return;
            }

            DbSet.Remove(existingEntity);
        }
    }
}