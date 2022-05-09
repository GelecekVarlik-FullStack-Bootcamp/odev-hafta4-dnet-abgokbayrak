using BMS.Dal.Abstract;
using BMS.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BMS.Dal.Concrete.EntityFramework.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> dbSet;
        

        public GenericRepository(DbContext dbContext)
        {
            this.context = dbContext;
            dbSet = this.context.Set<T>();
        }


        public async Task<T> AddAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Added;
            await context.AddAsync(entity);
            return entity;
        }

        public T Add(T entity)
        {
            context.Entry(entity).State = EntityState.Added;
            context.Add(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            return Delete(Find(id));
        }

        public bool Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.Attach(entity);
            }

            return dbSet.Remove(entity) != null;
        }

        public T Find(int id)
        {
            var entity = dbSet.Find(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).ToList();
        }

        public IQueryable<T> GetQueryable()
        {
            return dbSet.AsQueryable();
        }

        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }

        public T SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            return dbSet.AsNoTracking().SingleOrDefault(expression);
        }
    }
}