using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Twitter.Repository.Interfaces;

namespace Twitter.Repository.classes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #region Get All Data Methods
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }


        public IQueryable<T> GetAllSorted<TKey>(Expression<Func<T, TKey>> sortingExpression)
        {
            return _dbSet.OrderBy<T, TKey>(sortingExpression);
        }

        public IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }

        public bool GetAny(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet;
            bool result = false;
            if (filter != null)
            {
                result = query.Any(filter);
            }
            return result;
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _dbSet.FirstOrDefault(filter);
            }
            return null;
        }


        #endregion
        #region Get one record
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T GetById(long id)
        {
            return _dbSet.Find(id);
        }

        #endregion
        #region CRUD Methods
        public virtual bool Insert(T entity)
        {
            bool returnVal = false;
            EntityEntry<T> dbEntityEntry = _context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(entity);
            }
            returnVal = true;
            return returnVal;
        }

        public virtual void InsertList(List<T> entityList)
        {
            _dbSet.AddRange(entityList);
        }

        public virtual void Update(T entity)
        {
            EntityEntry<T> dbEntityEntry = _context.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void UpdateList(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                Update(item);
            }
        }

        public virtual void Delete(T entity)
        {
            EntityEntry<T> dbEntityEntry = _context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }
        public virtual void DeleteList(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                Delete(item);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }
        #endregion
    }
}
