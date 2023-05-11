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
    public class CategoryServices : ICategoryServies
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Category> AddNew(Category category)
        {
            var cate = await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveChanges();
            return cate;
        }

        public async Task Delete(Guid id)
        {
                await _unitOfWork.CategoryRepository.DeleteById(id);
                await _unitOfWork.SaveChanges();
        }

        public async Task Edit(Category category)
        {
            await _unitOfWork.CategoryRepository.Edit(category);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetAllCategories(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = null)
        {
            var listCate = _unitOfWork.CategoryRepository.GetAll(filter, orderBy, includeProperties);
            return listCate;
        }

        public Task<Category> GetByCategoryId(Guid id)
        {
            var cate = _unitOfWork.CategoryRepository.GetById(id);
            return cate;
        }
    }
}
