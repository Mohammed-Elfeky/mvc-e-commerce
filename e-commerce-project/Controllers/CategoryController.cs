using Microsoft.AspNetCore.Mvc;
using e_commerce_project.viewModels.categories;
using e_commerce_project.Models;
using e_commerce_project.Repos;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce_project.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class CategoryController : Controller
    {
        ICategoryRepo cat;
        IWebHostEnvironment WebHostEnvironment;
        public CategoryController(ICategoryRepo _cat,IWebHostEnvironment _WebHostEnvironment)
        {
            cat= _cat;
            WebHostEnvironment=_WebHostEnvironment;
        }

        public IActionResult isUnique(string Name)
        {
            return cat.FindByName(Name) == true ? Json(false):Json(true);
        }


        public IActionResult Index()
        {
            List<Category> cats = cat.GetAll();
            return View(cats);
        }

        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(AddCategoryVM cat)
        {
            if (ModelState.IsValid == false)
            {
                return View("New",cat);
            }

            #region store image on server
            string categoriesImages = Path.Combine(WebHostEnvironment.WebRootPath, "categoriesImages");
            string UniqueimgName = Guid.NewGuid().ToString() + "_" + cat.img.FileName;
            string imgPath = Path.Combine(categoriesImages, UniqueimgName);
            using (var fileStream = new FileStream(imgPath, FileMode.Create))
            {
                cat.img.CopyTo(fileStream);
                fileStream.Close();
            }
            #endregion

            this.cat.Insert(new Category() { Name=cat.Name,img= UniqueimgName, Description=cat.Description});
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Category catToEdit = cat.FindById(id);
            if(catToEdit != null)
            {
                return View(catToEdit);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Category cat)
        {
            if (ModelState.IsValid==false)
            {
                return View("Edit",cat);
            }
            this.cat.Edit(cat.Id, cat);
            return RedirectToAction("Index");
        }

        public IActionResult delete(int id)
        {
            this.cat.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
