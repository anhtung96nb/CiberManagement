namespace CiberManagement.DAL.IRepositories
{
    using CiberManagement.DAL.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> Add(T obj);
        Task DeleteById(object id);
        Task Edit(T obj);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);
        Task<T> GetById(object id);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null,string includeProperties = null);
        Task<PageView<T>> GetWithPaging(int pageSize, int pageIndex, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);
    }
}
