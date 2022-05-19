using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace e_commerce_project.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Column(TypeName ="varchar(50)")]

        [Required]
        public string Name { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string img { get; set; }

        public virtual List<Product> products { get; set; }
    }
}
