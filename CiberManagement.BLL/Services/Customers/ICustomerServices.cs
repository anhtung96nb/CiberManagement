using CiberManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CiberManagement.BLL.Services
{
    public interface ICustomerServices
    {
        Task<Customer> GetByCustomerId(Guid id);
        Task<IEnumerable<Customer>> GetAllCustomers(Expression<Func<Customer, bool>> filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null, string includeProperties = null);
        Task<Customer> AddNew(Customer customer);

        Task Delete(Guid id);
        Task Edit(Customer customer);
    }
}
