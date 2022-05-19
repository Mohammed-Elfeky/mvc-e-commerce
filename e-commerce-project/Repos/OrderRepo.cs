using System.Collections.Generic;
using e_commerce_project.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using e_commerce_project.Models.postRequestsModels;
using System;

namespace e_commerce_project.Repos
{
    public class OrderRepo : IOrderRepo
    {
        context db;
        public OrderRepo(context db)
        {
            this.db = db;
        }


        public List<Order> GetOrdersById(string userId)
        {
            return db.Orders.Include(o=>o.User).Where(o => o.user_id == userId && o.submitted == true).ToList();

        }

        public List<Order> allOrders()
        {
            return db.Orders.Include(o => o.User).Where(o => o.submitted == true).ToList();

        }

        public int Edit(int id, Address address)
        {
            Order ordr = db.Orders.Find(id);
            if (ordr != null)
            {
                ordr.oLat = address.lat;
                ordr.oLong = address.longg;
                ordr.orderLocation = address.name;
                int raw = db.SaveChanges();
                return raw;
            }
            return 0;
        }

        public List<Order> orderByDate(string type)
        {
            if (type=="asc")
            {
                return db.Orders.Include(o => o.User).OrderBy(o => o.date).ToList();
            }
            return db.Orders.Include(o => o.User).OrderByDescending(o => o.date).ToList();
        }

        public List<Order> orderByTotal(string type)
        {
            if (type == "asc")
            {
                return db.Orders.Include(o => o.User).OrderBy(o => o.TotalPrice).ToList();
            }
            return  db.Orders.Include(o => o.User).OrderByDescending(o => o.TotalPrice).ToList();
        }

        public List<ProductGroub> CountOfSalesOfEachProduct()
        {
            return db.OrdersProducts.Include(op=>op.Product).
                    ToList().
                    GroupBy(op=>op.Product.Name).
                    Select(g=>new ProductGroub() { num=g.Sum(op=>op.quantity*op.Product.Price),productName=g.Key}).ToList();
        }
    }
}
