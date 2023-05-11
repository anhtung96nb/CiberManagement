using CiberManagement.DAL.DataContext;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;


namespace CiberManagement.DAL.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private CiberDBContext _context;
        public ProductRepository(CiberDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
