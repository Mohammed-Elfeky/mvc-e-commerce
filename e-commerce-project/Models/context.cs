using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_project.Models
{
    public class context: IdentityDbContext<appUser>
    {
        public context(DbContextOptions options) : base(options){}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderProduct>()
                .HasKey(or => new { or.OrderId, or.ProductId });
        }
        public virtual DbSet <Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrdersProducts { get; set; }

    }
}
