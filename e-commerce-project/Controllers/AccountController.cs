using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using e_commerce_project.Models;
using System.Threading.Tasks;
using e_commerce_project.viewModels.account;
namespace e_commerce_project.Controllers
{
    public class AccountController : Controller
    {
        #region code
        private readonly UserManager<appUser> userManager;
        private readonly SignInManager<appUser> signInManager;

        public AccountController(
            UserManager<appUser> _userManager
            , SignInManager<appUser> _signInManager
            )
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult signUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> signUp(signUPvm signUPvm)
        {
            if (ModelState.IsValid == true)
            {
                appUser userModel = new appUser();
                userModel.UserName = signUPvm.UserName;
                userModel.Address = signUPvm.Address;
                userModel.PasswordHash = signUPvm.Password;
                IdentityResult result = await userManager.CreateAsync(userModel, signUPvm.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Products", "ShowProduct");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(signUPvm);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(signINvm userVM)
        {
            if (ModelState.IsValid == true)
            {
                appUser userModel = await userManager.FindByNameAsync(userVM.UserNAme);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found == true)
                    {
                        await signInManager.SignInAsync(userModel, true);
                        return RedirectToAction("Products", "ShowProduct");
                    }
                }
                ModelState.AddModelError("", "Name & password Not Correct");
            }
            return View(userVM);
        }
        public async Task<IActionResult> signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Products", "ShowProduct");
        }
        #endregion
    }
}
