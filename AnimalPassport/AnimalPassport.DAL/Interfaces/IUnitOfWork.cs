using System;
using System.Threading.Tasks;
using AnimalPassport.Entities.Entities;

namespace AnimalPassport.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        Task SaveChangesAsync();
    }
}