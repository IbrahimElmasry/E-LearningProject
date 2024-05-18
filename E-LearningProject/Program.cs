using E_LearningProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllersWithViews();

// Configure database context
var connectionString = builder.Configuration.GetConnectionString("Connstr");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true; // No digit required
    options.Password.RequireLowercase = true; // No lowercase letter required
    options.Password.RequireUppercase = true; // No uppercase letter required
    options.Password.RequireNonAlphanumeric = false; // No special character required
    options.Password.RequiredLength = 7; // Minimum length set to 1
})
.AddEntityFrameworkStores<ApplicationDBContext>();

var app = builder.Build();

// Configure middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Handle errors with a custom error page
    app.UseHsts(); // Use HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files like CSS, JavaScript, and images

app.UseRouting(); // Enable endpoint routing

app.UseAuthorization(); // Authentication middleware

// Map default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
