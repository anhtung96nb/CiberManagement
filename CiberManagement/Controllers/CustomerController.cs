using CiberManagement.BLL.Services;
using CiberManagement.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiberManagement.Controllers
{
  
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerServices _customerService;

        public CustomerController(ICustomerServices customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _customerService.GetAllCustomers();

            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                TempData["InforCreate"] = "Create fail";
                return View(customer);
            }

            var customerNew = await _customerService.AddNew(customer);

            if (customerNew != null)
            {
                TempData["InforCreate"] = "Create success";
                return RedirectToAction("Index", "Customer");
            }
            return View(customerNew);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryVm = await _customerService.GetByCustomerId(id);

            if (categoryVm != null)
                return View(categoryVm);

            return RedirectToAction("Index", "Customer");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                TempData["InforUpdate"] = "Update fail";
                return View(customer);
            }

             await _customerService.Edit(customer);
            return View(customer);
        }
    }
}
