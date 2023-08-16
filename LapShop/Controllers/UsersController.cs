using LapShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signinManager;
        public UsersController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signinManager)
        {
            userManager = _userManager;
            signinManager = _signinManager;
        }
        public IActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //1234Tarek.
            /*if (!ModelState.IsValid)
                return View("Login", model);*/
            ApplicationUser user = new ApplicationUser()
            {

                Email = model.Email
            };
            try
            {
                var loginResult = await signinManager.PasswordSignInAsync(user.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {
                    if(string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect("/Home/Index");
                    return Redirect(model.ReturnUrl);
                }
                    
            }
            catch (Exception ex)
            {
                
            }


            return View(new LoginModel());
        }

        public IActionResult Register()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            //1234Tarek.
            if (!ModelState.IsValid)
                return View("Register", model);
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var loginResult = await signinManager.PasswordSignInAsync(user, model.Password, true, true);
                    var myUser = userManager.FindByEmailAsync(user.Email);
                    await userManager.AddToRoleAsync(user, "User");
                    if (loginResult.Succeeded)
                        return Redirect("/Order/OrderSuccess");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }


            return View(new UserModel());
        }

        public async Task<IActionResult> Logout()
        {
            await signinManager.SignOutAsync();
            return Redirect("~/");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
