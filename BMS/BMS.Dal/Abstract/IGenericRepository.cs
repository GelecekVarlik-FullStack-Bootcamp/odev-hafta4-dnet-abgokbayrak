using BMS.Entity.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BMS.Dal.Abstract
{
    public interface IGenericRepository<T> where T : IEntityBase
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        T SingleOrDefault(Expression<Func<T, bool>> expression);
        T Find(int id);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        bool Delete(int id);
        bool Delete(T entity);
        IQueryable<T> GetQueryable();
    }
}