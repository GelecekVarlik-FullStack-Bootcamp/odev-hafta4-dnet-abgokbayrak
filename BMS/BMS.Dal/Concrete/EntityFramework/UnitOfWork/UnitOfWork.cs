using BMS.Dal.Abstract;
using BMS.Dal.Concrete.EntityFramework.Repository;
using BMS.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace BMS.Dal.Concrete.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        DbContext dbContext;
        IDbContextTransaction transaction;
        bool dispose;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool BeginTransaction()
        {
            try
            {
                transaction = dbContext.Database.BeginTransaction();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            dispose = true;
        }

        public IGenericRepository<T> GetRepository<T>() where T : EntityBase
        {
            return new GenericRepository<T>(dbContext);
        }

        public bool RollBackTransaction()
        {
            try
            {
                transaction.Rollback();
                transaction = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int SaveChanges()
        {
            var _transaction = transaction != null ? transaction : dbContext.Database.BeginTransaction();
            using (_transaction)
            {
                try
                {
                    if (dbContext == null)
                        throw new ArgumentException("Context is null");

                    var result = dbContext.SaveChanges();

                    _transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error on save changes : " + ex.Message);
                }
            }
        }
    }
}