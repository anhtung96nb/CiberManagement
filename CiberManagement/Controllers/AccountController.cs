using CiberManagement.BLL.Services;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CiberManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerServices _customerServices;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountRepository accountRepository,IConfiguration configuration, ICustomerServices customerServices)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _customerServices = customerServices;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp signUp)
        {
            var result = await _accountRepository.SignUpAsync(signUp);
            if (result.Succeeded)
            {


                await _customerServices.AddNew(new Customer()
                {
                    Name = "",
                    Address = "",
                    ID = Guid.NewGuid()
                });

                return RedirectToAction("Index","Home");
            }
            return Unauthorized();
        }

        [HttpGet]
        public IActionResult SignIn()
        {


            var claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn signIn)
        {
            try
            {
                //var token = await _accountRepository.SignInAsync(signIn);
                var token = await _accountRepository.SignInAsync(signIn);
                if (!string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("SignIn", "Home");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            
        }
        public async Task<IActionResult> SignOut()
        {
            try
            {
                await _accountRepository.SignOutAsync();
                return RedirectToAction("SignIn", "Account");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
    }
}
