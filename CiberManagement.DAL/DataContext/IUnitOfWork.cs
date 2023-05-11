using CiberManagement.DAL.IRepositories;

namespace CiberManagement.DAL.DataContext
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }

        IProductRepository ProductRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IOrderRepository OrderRepository { get; }

        CiberDBContext CiberDBContext { get; }

        Task SaveChanges();
        Task Dispose();
    }
}
