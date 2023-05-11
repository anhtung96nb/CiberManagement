using CiberManagement.BLL.Services;
using CiberManagement.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace CiberManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IActionResult> Index()
        {
            var listProd = await _productServices.GetAllProducts(includeProperties:"Category");
            return View(listProd);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNew(Product product)
        {
            try
            {
                var prod = await _productServices.AddNew(product);
                return Ok(prod);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
           
        }
    }
}
