using CiberManagement.DAL.DataContext;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;


namespace CiberManagement.DAL.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private CiberDBContext _context;
        public OrderRepository(CiberDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
