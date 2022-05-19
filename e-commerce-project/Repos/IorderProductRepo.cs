using e_commerce_project.Models;
using System.Collections.Generic;

namespace e_commerce_project.Repos
{
    public interface IorderProductRepo
    {
        List<OrderProduct> getOrderProducts(int id);
        List<ProductChart> getProductCount();
        List<ProductChart> getProductTotalPrice();
    }
}