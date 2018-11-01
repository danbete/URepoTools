using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DBTools
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// add Entity Using EF6 Add()
        /// </summary>
        /// <param name="entity">the Entity to update</param>
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        /// <summary>
        /// gets IEnumerable<typeparamref name="T"/> 
        /// will add them to db using EF6 AddRange()
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }
        /// <summary>
        /// get entity to remove
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
        /// <summary>
        ///  gets IEnumerable<typeparamref name="T"/> 
        /// will remove them to db using EF6 RemoveRange()
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }
        /// <summary>
        /// will get predicate and will return an IEnumerable
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where<T>(predicate).AsEnumerable();
        }
        /// <summary>
        /// retrive all the items from the db
        /// </summary>
        /// <returns>all items as IEnumerable<typeparamref name="T"/></returns>
        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        /// <summary>
        /// get a PrimeryKey of type <typeparamref name="P"/>
        /// return a <typeparamref name="T"/> 
        /// </summary>
        /// <typeparam name="P">Primery Key type</typeparam>
        /// <param name="PrimeryKey">Primery Key value</param>
        /// <returns></returns>
        public T GetByPK<P>(P PrimeryKey)
        {
            return _dbContext.Set<T>().Find(PrimeryKey);
        }
        /// <summary>
        /// retrive IEnumerable<<typeparamref name="T"/>> of items for 
        /// </summary>
        /// <typeparam name="PK">the items will be ordered by this property key</typeparam>
        /// <param name="pageSize">number of items to retrive</param>
        /// <param name="skip">number of item to skip over</param>
        /// <param name="orderBy">like: (a) => a.id</param>
        /// <param name="Ascending">default = true</param>
        /// <returns>IEnumerable<<typeparamref name="T"/>></returns>
        public IEnumerable<T> GetPaging<PK>(int pageSize, int skip, Expression<Func<T, PK>> orderBy, bool  Ascending = true)
        {
            if(Ascending)
                return _dbContext.Set<T>().OrderBy(orderBy).Skip(skip).Take(pageSize).AsEnumerable();
            return _dbContext.Set<T>().OrderByDescending(orderBy).Skip(skip).Take(pageSize).AsEnumerable();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="PK">the items will be ordered by this property key</typeparam>
        /// <param name="pageSize">number of items to retrive</param>
        /// <param name="skip">number of item to skip over</param>
        /// <param name="orderBy">like: (a) => a.id</param>
        /// <param name="predicate">predicate like: (a) => a.Id >10 </param>
        /// <param name="Ascending">default = true</param>
        /// <returns>IEnumerable<<typeparamref name="T"/>></returns>
        /// 
        public IEnumerable<T> GetPaging<PK>(int pageSize, int skip, Expression<Func<T, PK>> orderBy, Expression<Func<T, bool>> predicate, bool Ascending = true)
        {
            if (Ascending)
                return _dbContext.Set<T>().Where(predicate).OrderBy(orderBy).Skip(skip).Take(pageSize).AsEnumerable();
            return _dbContext.Set<T>().Where(predicate).OrderByDescending(orderBy).Skip(skip).Take(pageSize).AsEnumerable();
        }
    }
}
