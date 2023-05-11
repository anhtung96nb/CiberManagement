using CiberManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CiberManagement.BLL.Services
{
    public interface IProductServices
    {
        Task<Product> GetByCategoryId(Guid id);
        Task<IEnumerable<Product>> GetAllProducts(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = null);
        Task<Product> AddNew(Product product);

        Task Delete(Guid id);
        Task Edit(Product product);
    }
}
