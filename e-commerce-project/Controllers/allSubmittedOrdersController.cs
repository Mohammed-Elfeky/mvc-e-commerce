using Microsoft.AspNetCore.Mvc;
using e_commerce_project.Repos;
using System.Linq;
using System.Security.Claims;
using e_commerce_project.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce_project.Controllers
{
    [Authorize]
    public class allSubmittedOrdersController : Controller
    {
        private readonly IOrderRepo orderRepo;
        private readonly IorderProductRepo iorderProductRepo;

        public allSubmittedOrdersController(IOrderRepo orderRepo,IorderProductRepo iorderProductRepo)
        {
            this.orderRepo = orderRepo;
            this.iorderProductRepo = iorderProductRepo;
        }
        public IActionResult Index()
        {
            string userId = User.
            Claims.
            FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            .Value;

            var ordersList = orderRepo.GetOrdersById(userId);
            return View(ordersList);
        }

        public IActionResult getOrderDetails(int id)
        {
            List<OrderProduct> products = iorderProductRepo.getOrderProducts(id).ToList();
            return View(products);
        }

        public IActionResult searchCity(int id)
        {

            List<OrderProduct> products = iorderProductRepo.getOrderProducts(id).ToList();
            return View(products);
        }
    }
}
