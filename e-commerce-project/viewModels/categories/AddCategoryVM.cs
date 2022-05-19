using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_project.viewModels.categories
{
    public class AddCategoryVM
    {
        [Required]
        [Remote("isUnique", "Category", ErrorMessage = "the category name is used before")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public IFormFile img { get; set; }
    }
}
