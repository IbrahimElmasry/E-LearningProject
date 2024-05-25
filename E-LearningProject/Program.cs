using E_LearningProject.MiddleWares;
using E_LearningProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Or your desired timeout
    options.SlidingExpiration = true;
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true; // Prevent JavaScript access
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.LoginPath = "/Account/Login"; // Redirect to Login on failure
    options.LogoutPath = "/Account/Logout"; // Redirect to Logout
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect on access denied
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter; // Use the default return URL parameter
    options.SlidingExpiration = true;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
});
var connectionString = builder.Configuration.GetConnectionString("Connstr");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Adjust password requirements based on your security needs
    // ...
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders(); // For token generation (e.g., email confirmation)

// Configure Middleware Options
builder.Services.Configure<SessionLoggingMiddlewareOptions>(builder.Configuration.GetSection("SessionLogging"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // UseSession MUST come before UseAuthentication and the middleware

// Add the middleware using app.Use()
app.Use(async (context, next) =>
{
    var middleware = context.RequestServices.GetService<SessionLoggingMiddleware>();
    if (middleware != null)
    {
        await middleware.InvokeAsync(context);
    }
    else
    {
        await next(context);
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
