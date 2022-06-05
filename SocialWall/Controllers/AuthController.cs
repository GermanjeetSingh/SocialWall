using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialWall.DAL;
using SocialWall.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialWall.Controllers
{
    public class AuthController : Controller
    {
                
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User loginUser)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager
                    .PasswordSignInAsync(loginUser.Username,loginUser.Password,false,false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username/Password");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User registerUser)
        {
            
            if (ModelState.IsValid)
            {
                if(!registerUser.Password.Equals(registerUser.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Your Password(s) should be same");
                    return View();
                }
                var user = new ApplicationUser();
                user.UserName = registerUser.Username;
                var result = await userManager.CreateAsync(user, password : registerUser.Password);
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user,isPersistent : false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var e in result.Errors)
                    {
                        ModelState.AddModelError("", e.Description);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

