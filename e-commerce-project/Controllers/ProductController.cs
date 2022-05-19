using Microsoft.AspNetCore.Mvc;
using e_commerce_project.Repos;
using System.Collections.Generic;
using e_commerce_project.Models;
namespace e_commerce_project.Controllers
{
    public class ShowProductController : Controller
    {
        IProductRepo productRepo;
        private readonly IorderProductRepo orderProductRepo;

        public ShowProductController(IProductRepo productRepo,IorderProductRepo orderProductRepo)
        {
            this.productRepo = productRepo;
            this.orderProductRepo = orderProductRepo;
        }
        
        public IActionResult Products()
        {
            List<Product> products=productRepo.GetAll();
            return View(products);
        }

        public IActionResult getProductById(int id)
        {
            Product product=productRepo.FindById(id);
            return View(product);
        }


        public IActionResult search(string name)
        {
            List<Product> products = productRepo.searchByName(name);
            return Json(products);
        }
        public IActionResult getProductsBYcategoryId(int id)
        {
            List<Product> products = productRepo.getProductsBYcategoryId(id);
            return View("Products",products);
        }

        public IActionResult filterByPrice(decimal price)
        {
            return Json(productRepo.getProductsBYPrice(price));
        }

        public IActionResult getProductCount()
        {
            return Json(orderProductRepo.getProductCount());
        }
        
        public IActionResult getProductTotalPrice()
        {
            return Json(orderProductRepo.getProductTotalPrice());
        }
    }
}
