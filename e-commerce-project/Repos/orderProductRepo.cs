using System.Collections.Generic;
using e_commerce_project.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_project.Repos
{
    public class orderProductRepo : IorderProductRepo
    {
        context db;
        public orderProductRepo(context db)
        {
            this.db = db;
        }

        public List<OrderProduct> getOrderProducts(int id)
        {
            return db.OrdersProducts.Include(op => op.Product).Where(op => op.OrderId == id).ToList();
        }

        public List<ProductChart> getProductCount()
        {
            return db.OrdersProducts.Include(op => op.Product)
                     .GroupBy(op => op.Product.Name)
                     .Select(g => new ProductChart() { productName = g.Key,count= g.Sum(op=>op.quantity) })
                     .ToList();
        }
        public List<ProductChart> getProductTotalPrice()
        {
            return db.OrdersProducts.Include(op => op.Product).
                      ToList()
                     .GroupBy(op => op.Product.Name)
                     .Select(g => new ProductChart() { productName = g.Key, count = g.Sum(op => op.quantity*op.Product.Price) })
                     .ToList();
        }
    }
}
