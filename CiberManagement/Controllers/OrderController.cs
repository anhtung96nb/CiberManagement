using CiberManagement.BLL.Services;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CiberManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly IProductServices _productServices;

        public OrderController(IOrderServices orderServices, IProductServices productServices)
        {
            _orderServices = orderServices;
            _productServices = productServices;
        }

        public async Task<IActionResult> Index(string keyword = null, string sortOrder = null, int pageIndex = 1, int pageSize = 5)
        {
            Expression<Func<Order, bool>> filter = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                ViewBag.Keyword = keyword;
                keyword = keyword.ToLower();
                filter = x => x.Product.Name.ToLower().Contains(keyword) || x.Product.Category.Name.ToLower().Contains(keyword);
            }

            ViewBag.ProductSort = sortOrder == "product_desc" ? "product_asc" : "product_desc";
            ViewBag.CategorySort = sortOrder == "category_desc" ? "category_asc" : "category_desc";
            ViewBag.CustomerSort = sortOrder == "customer_desc" ? "customer_asc" : "customer_desc";

            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null;
            switch (sortOrder)
            {
                case "product_desc":
                    orderBy = x => x.OrderByDescending(x => x.Product.Name);
                    break;
                case "product_asc":
                    orderBy = x => x.OrderBy(x => x.Product.Name);
                    break;
                case "category_desc":
                    orderBy = x => x.OrderByDescending(x => x.Product.Category.Name);
                    break;
                case "category_asc":
                    orderBy = x => x.OrderBy(x => x.Product.Category.Name);
                    break;
                case "customer_desc":
                    orderBy = x => x.OrderByDescending(x => x.Customer.Name);
                    break;
                case "customer_asc":
                    orderBy = x => x.OrderBy(x => x.Customer.Name);
                    break;
                default:
                    orderBy = x => x.OrderByDescending(x => x.Product.Name);
                    break;
            }

            var orders = await _orderServices.GetWithPaging(pageSize,pageIndex, filter,orderBy:orderBy,includeProperties: "Customer,Product.Category");

            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Create(Guid producId)
        {
            TempData["product"] = await _productServices.GetByCategoryId(producId);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Create", "Order", new { producId =order.ProductID});
                TempData["ResultCreatedOrder"] = "Create Fail";
            }
            order.OrderDate = DateTime.Now;
            var orderNew =await _orderServices.AddNew(order);
            if (orderNew != null)
            {
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Error", "Home");
           

        }
    }
}
