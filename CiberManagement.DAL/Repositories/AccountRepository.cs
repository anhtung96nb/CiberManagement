using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CiberManagement.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountRepository(UserManager<User> userManager,SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<string> SignInAsync(SignIn signIn)
        {
            var user = await _userManager.FindByNameAsync(signIn.Email);
            var result = await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password, false, false);
            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var authenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,signIn.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };
            var roleUser = await _userManager.GetRolesAsync(user);
            if (roleUser!= null)
            {
                foreach (var role in roleUser)
                {
                    authenClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            JwtSecurityToken jwtSecurityToken = GetToken(authenClaims);
            var jwtTK = new JwtSecurityTokenHandler();
            var token = jwtTK.WriteToken(jwtSecurityToken);
            return token;
        }
        public async Task<IdentityResult> SignUpAsync(SignUp signUp)
        {
            var user = new User
            {
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                Email = signUp.Email,
                UserName = signUp.Email
            };
            return await _userManager.CreateAsync(user, signUp.Password);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var keySer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecuriteKey"]));

            var token = new JwtSecurityToken(

               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               expires: DateTime.Now.AddMinutes(30),
               claims: claims,
               signingCredentials: new SigningCredentials(keySer, SecurityAlgorithms.HmacSha512Signature)
               );
            return token;
        }
    }
}
