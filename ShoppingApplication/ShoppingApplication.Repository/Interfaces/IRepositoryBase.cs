using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShoppingApplication.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        #region Get
        IQueryable<T> GetAll();
        T GetItem(int id);
        #endregion

        #region Find
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T Find(Expression<Func<T, bool>> match);
        #endregion

        #region Delete
        void Delete(T entity);
        void Delete(int entityId);
        #endregion

        #region Add
        void Create(T entity);
        #endregion

        #region Save
        void SaveChanges();
        #endregion

    }
}
