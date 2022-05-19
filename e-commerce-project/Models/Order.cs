using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace e_commerce_project.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string address { get; set; }

        public decimal TotalPrice { get; set; }
        
        public DateTime date { get; set; }

        public Boolean submitted { get; set; }

        public Boolean deliverd { get; set; }

        
        [Column(TypeName = "varchar(50)")]
        public string aLat { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string aLong { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string oLat { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string oLong { get; set; }
        public string orderLocation { get; set; }

        [ForeignKey("User")]
        public string user_id { get; set; }

        public virtual appUser User { get; set; }

        //public virtual List<OrderProduct> orderProducts { get; set; }
    }
}
