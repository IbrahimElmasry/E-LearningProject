using E_LearningProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningProject.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDBContext _dbContext;
        public AccountController(UserManager<IdentityUser> userManager,
                    RoleManager<IdentityRole> roleManager,
                     SignInManager<IdentityUser> signInManager,
                     ApplicationDBContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _dbContext = dbContext;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDto use)
        {
            if(ModelState.IsValid)
            {

                var res= await _userManager.FindByEmailAsync(use.Email);
                if(res.Email is  null)
                {
                    var St = new IdentityUser
                    {
                        UserName = use.FristName + use.LastName,
                        Email = use.Email

                    };
                    var Result = await _userManager.CreateAsync(St, use.Password);
                    var re = _dbContext.SaveChanges();
                    if (Result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", use);
                    }
                }
                else
                {
                    return View(use);
                }
               
            }

            return View(use);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDto use)
        {
            if (ModelState.IsValid)
            {
                
                var  user = await _userManager.FindByEmailAsync(use.Email);

                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, use.Password);
                    if (found)
                    {
                        
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Email or Password.");
            }
            return View(use);
        }


    }
}
