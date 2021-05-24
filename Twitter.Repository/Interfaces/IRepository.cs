using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        #region Get All Data Methods
        public IQueryable<T> GetAll();
        public IQueryable<T> GetAllSorted<TKey>(Expression<Func<T, TKey>> sortingExpression);
        public IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> filter, string includeProperties);
        public bool GetAny(System.Linq.Expressions.Expression<Func<T, bool>> filter);
        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter);
        #endregion

        #region Get one record
        public T GetById(int id);
        public T GetById(long id);
        #endregion

        #region CRUD Methods
        public bool Insert(T entity);
        public void InsertList(List<T> entityList);
        public void Update(T entity);
        public void UpdateList(List<T> entityList);
        public void Delete(T entity);
        public void DeleteList(List<T> entityList);
        public void Delete(int id);
        #endregion
    }
}
