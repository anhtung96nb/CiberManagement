
using CiberManagement.BLL.Services;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiberManagement.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryServies _categoryServices;

        public CategoryController(ICategoryServies categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public IActionResult Index()
        {
            var cates = _categoryServices.GetAllCategories();
            return View(cates);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {
                var cate = await _categoryServices.GetByCategoryId(id);
                if (cate == null)
                {
                    return NotFound();
                }
                return Ok(cate);
            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }
        }
        public async Task<IActionResult> AddCategory(Category category)
        {
            try
            {
                var cate = await _categoryServices.AddNew(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
