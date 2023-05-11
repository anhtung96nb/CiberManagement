
using CiberManagement.DAL.DataContext;
using CiberManagement.DAL.Model;
using System.Linq.Expressions;

namespace CiberManagement.BLL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> AddNew(Order order)
        {
            var cate = await _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveChanges();
            return cate;
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.OrderRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
        }

        public async Task Edit(Order order)
        {
            await _unitOfWork.OrderRepository.Edit(order);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Order>> GetAllOrders(Expression<Func<Order, bool>> filter = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, string includeProperties = null)
        {
            var listOrder = _unitOfWork.OrderRepository.GetAll(filter, orderBy, includeProperties);
            return listOrder;
        }

        public Task<Order> GetByOrderId(Guid id)
        {
            var order = _unitOfWork.OrderRepository.GetById(id);
            return order;
        }

        public async Task<PageView<Order>> GetWithPaging(int pageSize, int pageIndex, Expression<Func<Order, bool>> filter = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, string includeProperties = null)
        {
            var orders =await _unitOfWork.OrderRepository.GetWithPaging(pageSize,pageIndex, filter, orderBy, includeProperties);
            return orders;
        }
    }
}
