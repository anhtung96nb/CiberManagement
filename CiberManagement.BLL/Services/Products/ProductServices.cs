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
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> AddNew(Product product)
        {
            var prod= await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChanges();
            return prod;
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.ProductRepository.DeleteById(id);
            await _unitOfWork.SaveChanges();
            
        }

        public async Task Edit(Product product)
        {
            await _unitOfWork.ProductRepository.Edit(product);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetAllProducts(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = null)
        {
            var lstProd= _unitOfWork.ProductRepository.GetAll(filter,orderBy,includeProperties);
            return lstProd;
        }

        
        public async Task<Product> GetByCategoryId(Guid id)
        {
            var prod = await _unitOfWork.ProductRepository.GetById(id);
            return prod;
        }
    }
}
