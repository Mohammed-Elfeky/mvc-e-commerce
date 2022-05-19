using Microsoft.AspNetCore.Mvc;
using e_commerce_project.Repos;
using Microsoft.AspNetCore.Authorization;
using e_commerce_project.Models.postRequestsModels;
using e_commerce_project.Models;
using System.Collections.Generic;
using System.Linq;
namespace e_commerce_project.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class OrderController : Controller
    {
        private readonly IOrderRepo orderRepo;
        private readonly IorderProductRepo iorderProductRepo;
        public OrderController(IOrderRepo orderRepo, IorderProductRepo iorderProductRepo)
        {
            this.orderRepo = orderRepo;
            this.iorderProductRepo = iorderProductRepo;
        }
        public IActionResult Index()
        {
            var orders = orderRepo.allOrders();
            return View(orders);
        }
        [HttpPost]
        public IActionResult editAddress(int id,[FromBody] Address address)
        {
            orderRepo.Edit(id, address);
            return Json(new { });
        }

        public IActionResult getOrderDetails(int id)
        {
            List<OrderProduct> products = iorderProductRepo.getOrderProducts(id).ToList();
            return View(products);
        }

        public IActionResult orderByDate(string type)
        {
            return Json(orderRepo.orderByDate(type));
        }
        public  IActionResult orderByTotal(string type)
        {
            List<Order> orders = orderRepo.orderByTotal(type);
            return Json(orders);
        }
        
        public IActionResult CountOfSalesOfEachProduct()
        {
            var q = orderRepo.CountOfSalesOfEachProduct();
            return Json(orderRepo.CountOfSalesOfEachProduct());
        }
    }
}
