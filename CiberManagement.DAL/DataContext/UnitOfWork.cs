using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Repositories;

namespace CiberManagement.DAL.DataContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly CiberDBContext _ciberDBContext;

        public UnitOfWork(CiberDBContext ciberDBContext)
        {
            _ciberDBContext = ciberDBContext;
        }
        public CiberDBContext CiberDBContext => _ciberDBContext;
        public ICategoryRepository CategoryRepository => _categoryRepository != null? _categoryRepository : new CategoryRepository(_ciberDBContext);

        public IProductRepository ProductRepository => _productRepository != null ? _productRepository : new ProductRepository(_ciberDBContext);

        public ICustomerRepository CustomerRepository => _customerRepository != null ? _customerRepository : new CustomerRepository(_ciberDBContext);

        public IOrderRepository OrderRepository => _orderRepository != null ? _orderRepository : new OrderRepository(_ciberDBContext);

        public async Task Dispose()
        {
            await _ciberDBContext.DisposeAsync();
        }

        public async Task SaveChanges()
        {
             await _ciberDBContext.SaveChangesAsync();
        }
    }
}
