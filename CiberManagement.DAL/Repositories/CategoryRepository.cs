
using CiberManagement.DAL.DataContext;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;

namespace CiberManagement.DAL.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private CiberDBContext _context;
        public CategoryRepository(CiberDBContext context) : base(context)
        {
            _context = context;
        }


    }
}
