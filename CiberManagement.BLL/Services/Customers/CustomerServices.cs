using CiberManagement.DAL.DataContext;
using CiberManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CiberManagement.BLL.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Customer> AddNew(Customer customer)
        {
            var cus = await _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.SaveChanges();
            return cus;

        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.CustomerRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
        }


        public async Task Edit(Customer customer)
        {
            await _unitOfWork.CustomerRepository.Edit(customer);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers(Expression<Func<Customer, bool>> filter = null, Func<IQueryable<Customer>, IOrderedQueryable<Customer>> orderBy = null, string includeProperties = null)
        {
            var listCustomer = _unitOfWork.CustomerRepository.GetAll(filter,orderBy,includeProperties);
            return listCustomer;
        }

        public async Task<Customer> GetByCustomerId(Guid id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(id);
            return customer;
        }
    }
}
