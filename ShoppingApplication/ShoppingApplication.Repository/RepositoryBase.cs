using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApplication.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ShopingContext ShoppingContext { get; set; }
        public RepositoryBase(ShopingContext shopingContext)
        {
            ShoppingContext = shopingContext;
        }
        public void Create(T entity)
        {
            ShoppingContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            ShoppingContext.Set<T>().Remove(entity);
        }

        public void Delete(int entityId)
        {
            ShoppingContext.Set<T>().Remove(ShoppingContext.Set<T>()?.Find(entityId));
        }

        public virtual T Find(System.Linq.Expressions.Expression<Func<T, bool>> match)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual System.Linq.IQueryable<T> GetAll()
        {
            return ShoppingContext.Set<T>();
        }

        public virtual T GetItem(int id)
        {
            return ShoppingContext.Set<T>().Find(id);
        }

        public virtual void SaveChanges()
        {
            ShoppingContext.SaveChanges();
        }
    }
}
