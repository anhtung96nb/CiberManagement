using CiberManagement.DAL.DataContext;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;


namespace CiberManagement.DAL.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private CiberDBContext _context;
        public CustomerRepository(CiberDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
