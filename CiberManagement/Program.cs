using CiberManagement.BLL.Services;
using CiberManagement.DAL.DataContext;
using CiberManagement.DAL.IRepositories;
using CiberManagement.DAL.Model;
using CiberManagement.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// Add DbContext

builder.Services.AddDbContext<CiberDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CiberDB")));

// Add Identity

builder.Services.AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<CiberDBContext>().AddDefaultTokenProviders();



// authentication with cookie
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {

        options.LoginPath = new PathString("/Account/SignIn");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        //options.ReturnUrlParameter = "RequestPath";
        options.Cookie = new CookieBuilder()
        {
            Name = "CiberAuthentication",
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.SameAsRequest,
        };
    });

//// authentication with JWT
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


//}).AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["JWT:ValidAudience"],
//        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecuriteKey"]))
//    };

//});
builder.Services.AddAutoMapper(typeof(Program));

//

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();




builder.Services.AddScoped<ICategoryServies, CategoryServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();

//Config Logging

var path = Directory.GetCurrentDirectory();
var logger = new LoggerConfiguration().WriteTo.File($"{path}\\Logs\\CiberLog-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Logging.AddSerilog(logger);
var app = builder.Build();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Account/SignIn");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.Run();
