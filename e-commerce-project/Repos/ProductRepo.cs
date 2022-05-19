using System.Collections.Generic;
using e_commerce_project.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_project.Repos
{
    public class ProductRepo : IProductRepo
    {
        context db;
        public ProductRepo(context db)
        {
            this.db = db;
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }
        public List<Product> GetproductandCategory()
        {
            return db.Products.Include(N => N.Category).ToList();
        }
        public List<Product> searchByName(string name)
        {
            return db.Products.Where(p=>p.Name.Contains(name)).ToList();
        }
        public Product FindById(int id)
        {
            return db.Products.FirstOrDefault(x => x.Id == id);
        }
        public int Insert(Product product)
        {
            db.Products.Add(product);
            int raw = db.SaveChanges();
            return raw;
        }
        public int Edit(int id, Product product)
        {
            Product prdct = FindById(id);
            if (prdct != null)
            {
                prdct.Name = product.Name;
                prdct.Price = product.Price;
                prdct.Description = product.Description;
                prdct.CategoryId = product.CategoryId;
                int raw = db.SaveChanges();
                return raw;
            }
            return 0;
        }
        public int Delete(int id)
        {
            Product prdct = FindById(id);
            db.Products.Remove(prdct);
            return db.SaveChanges();
        }


        public List<Product> getProductsBYcategoryId(int catId)
        {
            return db.Products.Where(p=>p.CategoryId==catId).ToList();
        }


        public List<Product> getProductsBYPrice(decimal price)
        {
            return db.Products.Where(p=>p.Price<=price).ToList();
        }
    }
}
