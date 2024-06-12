using E_LearningProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace E_LearningProject.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<AccountController> _logger; // Add ILogger

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDBContext dbContext,
            ILogger<AccountController> logger) // Inject 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email is already taken.");
                    return View(userDto);
                }

                var user = new IdentityUser
                {
                    UserName = userDto.Email, // Use email as username for simplicity
                    Email = userDto.Email
                };

                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    // You might want to consider email confirmation or other steps here

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Log errors for troubleshooting
            _logger.LogWarning("Registration failed with errors: {Errors}", ModelState);

            return View(userDto);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDtoLogin userDtoLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userDtoLogin.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, userDtoLogin.Password))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(userDtoLogin);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            return View(userDtoLogin);
        }
        public async Task<IActionResult> Logout()
        {
            // Log Session Before Logout (for debugging purposes)
            _logger.LogInformation("Session before logout: {Session}", HttpContext.Session?.Id); // Null-conditional check

            // Sign out from ASP.NET Core Identity
            await _signInManager.SignOutAsync();

            // Invalidate the Authentication Cookie
            if (Request.Cookies.ContainsKey(".AspNetCore.Identity.Application")) // Adjust cookie name if needed
            {
                Response.Cookies.Delete(".AspNetCore.Identity.Application", new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1), // Expire in the past
                    HttpOnly = true,
                    Secure = true // Use HTTPS if applicable
                });

                _logger.LogInformation("Authentication cookie deleted.");
            }
            else
            {
                _logger.LogWarning("Authentication cookie not found.");
            }

            // Clear the Session
            HttpContext.Session.Clear();

            // Log Session After Logout
            _logger.LogInformation("Session after logout: {Session}", HttpContext.Session?.Id); // Null-conditional check

            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }


    }
}
