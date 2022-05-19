using Microsoft.AspNetCore.Mvc;
using e_commerce_project.viewModels.editor;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using e_commerce_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce_project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EditorController : Controller
    {

        private readonly UserManager<appUser> userManager;
        private readonly SignInManager<appUser> signInManager;

        public EditorController(
            UserManager<appUser> _userManager
            , SignInManager<appUser> _signInManager
            )
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public async Task<IActionResult> Editors()
        {
            var editors=await userManager.GetUsersInRoleAsync("Editor");
            return View(editors);
        }

        public IActionResult AddEditor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAddEditor(AddEditorVM AddEditorVM)
        {
            if (ModelState.IsValid == true)
            {
                appUser userModel = new appUser();
                userModel.UserName = AddEditorVM.UserName;
                userModel.PasswordHash = AddEditorVM.Password;
                IdentityResult result = await userManager.CreateAsync(userModel, AddEditorVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "Editor");
                    //await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Editors", "Editor");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View("AddEditor",AddEditorVM);
        }


        public async Task<IActionResult> delete(string id)
        {
            appUser user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Editor");
            return RedirectToAction("Editors");
        }

    }
}
