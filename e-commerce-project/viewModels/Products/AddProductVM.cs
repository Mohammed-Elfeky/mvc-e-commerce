using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_project.viewModels.Products
{
    public class AddProductVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile img { get; set; }
        [Required]
        [Range(500,20000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
