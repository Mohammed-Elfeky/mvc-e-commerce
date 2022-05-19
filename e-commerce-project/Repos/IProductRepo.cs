using System.Collections.Generic;
using e_commerce_project.Models;

namespace e_commerce_project.Repos
{
    public interface IProductRepo
    {
        int Delete(int id);
        int Edit(int id, Product product);
        Product FindById(int id);
        List<Product> GetAll();
        int Insert(Product product);
        List<Product> GetproductandCategory();
        List<Product> searchByName(string name);
        List<Product> getProductsBYcategoryId(int catId);
        List<Product> getProductsBYPrice(decimal price);
    }
}