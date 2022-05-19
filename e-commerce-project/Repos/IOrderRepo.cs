using e_commerce_project.Models;
using System.Collections.Generic;
using e_commerce_project.Models.postRequestsModels;
namespace e_commerce_project.Repos
{
    public interface IOrderRepo
    {
        List<Order> GetOrdersById(string userId);
        List<Order> allOrders();
        int Edit(int id, Address address);
        List<Order> orderByDate(string type);
        List<Order> orderByTotal(string type);
        List<ProductGroub> CountOfSalesOfEachProduct();
    }
}