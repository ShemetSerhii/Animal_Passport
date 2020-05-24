using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPassport.DataAccess.Interfaces;
using AnimalPassport.DataAccess.Repositories;
using AnimalPassport.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalPassport.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDictionary<Type, object> _repositoryStorage;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _repositoryStorage = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositoryStorage.ContainsKey(typeof(TEntity)))
            {
                return _repositoryStorage[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(_context);
            _repositoryStorage.Add(typeof(TEntity), repository);

            return repository;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}