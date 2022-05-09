using BMS.Entity.Base;
using System;

namespace BMS.Dal.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : EntityBase;
        bool BeginTransaction();
        bool RollBackTransaction();
        int SaveChanges();
    }
}
