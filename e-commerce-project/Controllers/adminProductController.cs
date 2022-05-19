using Microsoft.AspNetCore.Mvc;
using e_commerce_project.Repos;
using e_commerce_project.Models;
using e_commerce_project.viewModels.Products;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce_project.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class productController : Controller
    {
        private readonly IProductRepo product;
        private readonly ICategoryRepo category;
        IWebHostEnvironment WebHostEnvironment;

        public productController(IProductRepo product, ICategoryRepo category, IWebHostEnvironment WebHostEnvironment)
        {
            this.product = product;
            this.category = category;
            this.WebHostEnvironment = WebHostEnvironment;
        }

        public IActionResult Index()
        {
            var allproduct = product.GetproductandCategory();
            return View(allproduct);
        }
        public IActionResult Delete(int id)
        {
            this.product.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult New()
        {
            ViewBag.AllCategories = this.category.GetAll();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(AddProductVM pro)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.AllCategories = this.category.GetAll();
                return View("New", pro);
            }

            #region store image on server
            string productsImages = Path.Combine(WebHostEnvironment.WebRootPath, "productImages");
            string UniqueimgName = Guid.NewGuid().ToString() + "_" + pro.img.FileName;
            string imgPath = Path.Combine(productsImages, UniqueimgName);
            using (var fileStream = new FileStream(imgPath, FileMode.Create))
            {
                pro.img.CopyTo(fileStream);
                fileStream.Close();
            }
            #endregion
            this.product.Insert(new Product() { Name = pro.Name, img = UniqueimgName, Price = pro.Price, Description = pro.Description, CategoryId = pro.CategoryId });
            return RedirectToAction("Index");
        }
    
        
        public IActionResult Edit(int id)
        {
            var oldProduct = this.product.FindById(id);
            if (oldProduct != null)
            {
                ViewBag.AllCategories = this.category.GetAll();
                return View("Edit",oldProduct);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveEdit(Product product)
        {
            if (ModelState.IsValid == false)
            {
                return View("Edit", product);
            }
            this.product.Edit(product.Id, product);
            return RedirectToAction("Index");
        }
    }
}
