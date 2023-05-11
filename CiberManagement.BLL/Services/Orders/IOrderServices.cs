using CiberManagement.DAL.Model;
using System.Linq.Expressions;

namespace CiberManagement.BLL.Services
{
    public interface IOrderServices
    {
        Task<Order> GetByOrderId(Guid id);
        Task<IEnumerable<Order>> GetAllOrders(Expression<Func<Order, bool>> filter = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, string includeProperties = null);
        Task<Order> AddNew(Order order);

        Task Delete(Guid id);
        Task Edit(Order order);
        Task<PageView<Order>> GetWithPaging(int pageSize, int pageIndex, Expression<Func<Order, bool>> filter = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, string includeProperties = null);

    }
}
