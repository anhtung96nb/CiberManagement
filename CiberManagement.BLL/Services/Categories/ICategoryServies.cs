using CiberManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CiberManagement.BLL.Services
{
   public interface ICategoryServies
    {
        Task<Category> GetByCategoryId(Guid id);
        Task<IEnumerable<Category>> GetAllCategories(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = null);
        Task<Category> AddNew(Category category);

        Task Delete(Guid id);
        Task Edit(Category category);

    }
}
