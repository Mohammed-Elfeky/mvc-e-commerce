using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_project.Models
{
    public class OrderProduct
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order  { get; set; }


    }
}
