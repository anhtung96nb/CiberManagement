

namespace CiberManagement.DAL.Repositories
{
    using CiberManagement.DAL.DataContext;
    using CiberManagement.DAL.IRepositories;
    using CiberManagement.DAL.Model;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private CiberDBContext _context;
        internal DbSet<T> dbSet;

        public RepositoryBase(CiberDBContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public async Task<T> Add(T obj)
        {
            dbSet.Add(obj);
            return obj;

        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;



            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }
        public async Task DeleteById(object id)
        {
            var entity = await dbSet!.FindAsync(id);
            dbSet.Remove(entity);

        }

        public async Task<T> GetById(object id)
        {
            return await dbSet!.FindAsync(id);
        }



        public async Task Edit(T obj)
        {
            _context.Entry<T>(obj).State = EntityState.Modified;
        }

        public  async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault(filter);
        }
        public async Task<PageView<T>> GetWithPaging(int pageSize, int pageIndex
            , Expression<Func<T, bool>> filter = null
            , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            int totalRows = query.Count();
            int totalPage = (int)Math.Ceiling((decimal)totalRows / pageSize);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    
                        query = query.Include(includeProp);
                    
                    
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return new PageView<T>(query, pageIndex, pageSize, totalPage);
        }

       

       
    }
}

